using System.Security.Claims;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Implementations;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;

namespace FINALPROJECT.Services.Implementations
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly ICarRepo _carRepo;
        private readonly IEmailSender _mail;
        private readonly ICustomerService _customerService;
        private readonly ICustomerRepo _customerRepo;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuctionService(IAuctionRepo auctionRepo, ICarRepo carRepo, IEmailSender mail, ICustomerService customerService, ICustomerRepo customerRepo, IHttpContextAccessor contextAccessor)
        {
            _auctionRepo = auctionRepo;
            _carRepo = carRepo;
            _mail = mail;
            _customerService = customerService;
            _customerRepo = customerRepo;
            _contextAccessor = contextAccessor;
        }

        public async Task<BaseResponse<AuctionResponseModel>> Create(AuctionRequestModel request)
        {
            if(DateTime.Now >= request.AuctionEndDate)
            {
                return new BaseResponse<AuctionResponseModel>
                {
                    Status = false,
                    Message = "Date Cannot be in the past"
                };
            }
            
            var check = await _auctionRepo.Exist(x => x.CarId == request.CarId);
            if(check)
            {
                return new BaseResponse<AuctionResponseModel>
                {
                    Status = false,
                    Message = "Car Has been auctioned before"
                };
            }

            var car = await _carRepo.Get(x => x.Id == request.CarId);
            var auction = new Auction
            {
                Name = car.Brand + car.Name + car.Model,
                AuctionStartDate = DateTime.Now,
                AuctionEndDate = request.AuctionEndDate,
                CarId = request.CarId,
                StartingPrice = request.StartingPrice,
                CurrentPrice = request.StartingPrice,
                Description = $"{car.Brand} {car.Name} {car.Model}. It has a colour of {car.Color} and its Chasis Number is {car.ChasisNumber} for verification on accident sites",
                ImageURL = car.ImageUrl,
                //Status = Domain.Enums.AuctionStatus.Active,
                Bids = new List<Bid>(),
            };
            await _auctionRepo.Add(auction);
            car.Auctions.Add(auction);
            await _auctionRepo.SaveAsync();
            return new BaseResponse<AuctionResponseModel>
            {
                Status = true,
                Data = new AuctionResponseModel
                {
                    Name = auction.Name,
                    //Status = auction.Status,
                    AuctionEndDate = auction.AuctionEndDate,
                    AuctionStartDate = auction.AuctionStartDate,
                    Bids = auction.Bids.Select(b => new BidResponseModel
                    {
                        Amount = b.Amount,
                        AuctionId = auction.Id,
                        CustomerId = b.CustomerId,
                        CustomerName = b.Customer.FirstName + b.Customer.LastName,
                    }).ToList(),
                    StartingPrice = auction.StartingPrice,
                    CarId = auction.CarId,
                    CurrentPrice = auction.CurrentPrice,
                    Description = auction.Description,
                    ImageURL = auction.ImageURL,
                }
            };

        }

        public async Task<BaseResponse<AuctionResponseModel>> DeleteAuction(string id)
        {
          var auction = await _auctionRepo.Get(x => x.Id == id);
            auction.IsDeleted = true;
            await _auctionRepo.Update(auction);
            return new BaseResponse<AuctionResponseModel>
            {
                Status = true,
                Message = "Auction Deleted Succesfully",
                Data = null,
            };
        }

        public async Task<BaseResponse<AuctionResponseModel>> EndAuction()
        {
            var auctions = await _auctionRepo.GetAllAsync();
            var check = new HashSet<Auction>();
            var winningbid = new Bid();
            var checkedbid = 0;

            foreach (var auction in auctions)
            {
                if (DateTime.Now >= auction.AuctionEndDate)
                {
                    //auction.Status = Domain.Enums.AuctionStatus.Closed;
                    auction.IsDeleted = true; 
                    check.Add(auction);
                    await _auctionRepo.Update(auction);
                    await _auctionRepo.SaveAsync();
                }
            }
            foreach (var auction in check)
            {
                if (auction.Bids.Count == 0)
                {
                    check.Remove(auction);
                }
            }
            if (check!=null)
            {
                foreach (var auction in check)
                {
                    var bids = auction.Bids;
                    foreach (var bid in bids)
                    {
                        if (bid.Amount > checkedbid)
                        {
                            winningbid = bid;
                        }
                    }
                    var customer = await _customerRepo.Get(x=>x.Id==winningbid.CustomerId);
                    customer.OutstandingPayments.Add(winningbid.Auction);
                   await  _customerRepo.Update(customer);
                   await  _customerRepo.SaveAsync();
                    var content = $"Dear {customer.LastName}, you have won the auction `{auction.Name}` with the bid {winningbid.Amount}. Please Navigate to our Won Auctions to make or cancel the payment";
                    var request = new EmailRequestModel
                    {
                        HtmlContent = content,
                        ToEmail = customer.Email,
                        Subject = "Won Auction",
                        ToName = customer.FirstName + " " + customer.LastName,
                    };
                    _mail.SendEMail(request);
                }
            }
            return new BaseResponse<AuctionResponseModel>
            {
                Status = true,
            };
        }

        public async Task<BaseResponse<ICollection<AuctionResponseModel>>> GetAllAuctions()
        {
            await EndAuction();
            //var userId = _contextAccessor.HttpContext.User.Claims
            //.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //var customer = await _customerRepo.Get(a => a.UserId == userId);
            var auctions = await _auctionRepo.GetAllAsync();
            if (auctions == null)
            {
                return new BaseResponse<ICollection<AuctionResponseModel>>
                {
                    Message = "There are no Auctions available",
                    Status = false,
                };
            }
            return new BaseResponse<ICollection<AuctionResponseModel>>
            {
                Status = true,
                Data = auctions.Select(a => new AuctionResponseModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    //Status = a.Status,
                    AuctionEndDate = a.AuctionEndDate,
                    AuctionStartDate = a.AuctionStartDate,
                    Bids = a.Bids.Select(b => new BidResponseModel
                    {
                        Amount = b.Amount,
                        AuctionId = b.AuctionId,
                        CustomerId = b.CustomerId,
                        CustomerName = b.Customer.FirstName + b.Customer.LastName,
                    }).ToList(),
                    StartingPrice = a.StartingPrice,
                    CarId = a.CarId,
                    CurrentPrice = a.CurrentPrice,
                    Description = a.Description,
                    ImageURL = a.ImageURL,
                    CustomerId = a.Id,
                }).ToList(),
            };

        }

        public async Task<BaseResponse<AuctionResponseModel>> GetAuction(string Id)
        {
            var a = await _auctionRepo.Get(x=> x.Id == Id);
            return new BaseResponse<AuctionResponseModel>
            {
                Status = true,
                Data = new AuctionResponseModel
                {
                    Name = a.Name,
                    //Status = a.Status,
                    AuctionEndDate = a.AuctionEndDate,
                    AuctionStartDate = a.AuctionStartDate,
                    Bids = a.Bids.Select(b => new BidResponseModel
                    {
                        Amount = b.Amount,
                        AuctionId = b.AuctionId,
                        CustomerId = b.CustomerId,
                        CustomerName = b.Customer.FirstName + b.Customer.LastName,
                    }).ToList(),
                    StartingPrice = a.StartingPrice,
                    CarId = a.CarId,
                    CurrentPrice = a.CurrentPrice,
                    Description = a.Description,
                    ImageURL = a.ImageURL,
                }
            };
        }

        public async Task<BaseResponse<AuctionResponseModel>> UpdateAuction(string id,AuctionRequestModel request)
        {
            var auction = await _auctionRepo.Get(x => x.Id == id);
            if (auction == null)
            {
                return new BaseResponse<AuctionResponseModel>
                {
                    Message = "Auction Not Found!",
                    Status = false
                };
            }
            auction.StartingPrice = double.Parse(request.StartingPrice.ToString() ?? auction.StartingPrice.ToString());
            auction.AuctionEndDate = DateTime.Parse(request.AuctionEndDate.ToString() ?? auction.AuctionEndDate.ToString());
           await  _auctionRepo.Update(auction);
            await _auctionRepo.SaveAsync();
            return new BaseResponse<AuctionResponseModel>
            {
                Message = "Auction Updated Successfully!",
                Status = true
            };
        }
        public async Task<BaseResponse<ICollection<BidResponseModel>>> GetBids(string Id)
        {
            var auction = await _auctionRepo.Get(x=>x.Id==Id);
            var Bids = auction.Bids;
            return new BaseResponse<ICollection<BidResponseModel>>
            {
                Data = Bids.Select(b => new BidResponseModel
                {
                    Amount = b.Amount,
                    AuctionId = b.AuctionId,
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer.FirstName,

                }).ToList(),
                Status = true,
            };
        }
        public async Task<BaseResponse<AuctionRequestModel>> GetAuctionUpdate(string Id)
        {
            var a = await _auctionRepo.Get(x => x.Id == Id);
            return new BaseResponse<AuctionRequestModel>
            {
                Status = true,
                Data = new AuctionRequestModel
                {
                 AuctionEndDate = a.AuctionEndDate,
                 StartingPrice= a.StartingPrice,
                }
            };
        }
    }
}
