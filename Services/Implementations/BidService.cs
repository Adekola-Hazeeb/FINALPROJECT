using System.Security.Cryptography;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;

namespace FINALPROJECT.Services.Implementations
{
    public class BidService : IBidService
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly ICustomerService _customerService;
        private readonly IBidRepo _bidRepo;
        public BidService(IAuctionRepo auctionRepo, ICustomerService customerService, IBidRepo bidRepo)
        {
            _auctionRepo = auctionRepo;
            _customerService = customerService;
            _bidRepo = bidRepo;
        }

        public async Task<BaseResponse<BidResponseModel>> Create( BidRequestModel request)
        {
            var auction = await _auctionRepo.Get(x => x.Id == request.AuctionId);
            var customer = await _customerService.GetCurrentCustomer();
            if (request.Amount < auction.CurrentPrice)
            {
                return new BaseResponse<BidResponseModel>()
                {
                    Status = false,
                    Message = "Bid must be higher than Current Bid"
                };
            }
            var bid = new Bid
            {
                Amount = request.Amount,
                AuctionId = request.AuctionId,
                Auction = auction,
                Customer = customer,
                CustomerId = customer.Id
            };
           await  _bidRepo.Add(bid);
            auction.Bids.Add(bid);
            auction.CurrentPrice = bid.Amount;
            customer.BidsMade.Add(bid);
            customer.AuctionsPartaken.Add(auction);
            await _bidRepo.SaveAsync();
            await _auctionRepo.SaveAsync();
            return new BaseResponse<BidResponseModel>
            {
                Data = new BidResponseModel
                {
                    Amount = bid.Amount,
                    AuctionId = bid.AuctionId,
                    CustomerId = bid.CustomerId,
                    CustomerName = bid.Customer.FirstName + bid.Customer.LastName,
                },
                Status = true,
            };
        }

        public async Task<BaseResponse<ICollection<BidResponseModel>>> GetAllBids()
        {
           var bids = await _bidRepo.GetAll();
            if (bids == null)
            {
                return new BaseResponse<ICollection<BidResponseModel>>
                {
                    Status = false,
                    Message = "There are no bids yet"
                };
            }
            return new BaseResponse<ICollection<BidResponseModel>>
            {
                Data = bids.Select(b => new BidResponseModel
                {
                    Amount = b.Amount,
                    AuctionId = b.AuctionId,
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer.FirstName + b.Customer.LastName,
                }).ToList(),
                Status = true,
            };
        }

        public async Task<BaseResponse<ICollection<BidResponseModel>>> GetAllBidsForAuction(string id)
        {
            var bids = await _bidRepo.GetAllForAuction(id);
            if (bids == null)
            {
                return new BaseResponse<ICollection<BidResponseModel>>
                {
                    Status = false,
                    Message = "There are no bids yet"
                };
            }
            return new BaseResponse<ICollection<BidResponseModel>>
            {
                Data = bids.Select(b => new BidResponseModel
                {
                    Amount = b.Amount,
                    AuctionId = b.AuctionId,
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer.FirstName + b.Customer.LastName,
                }).ToList(),
                Status = true,
            };
        }

        public async Task<BaseResponse<BidResponseModel>> GetBid(string id)
        {
            var bid = await _bidRepo.Get(x=> x.Id == id);
            if (bid == null)
            {
                return new BaseResponse<BidResponseModel>()
                {
                    Status = false,
                    Message = "Bid not found"
                };
            }
            return new BaseResponse<BidResponseModel>
            {
                Data = new BidResponseModel
                {
                      Amount = bid.Amount,
                    AuctionId = bid.AuctionId,
                    CustomerId = bid.CustomerId,
                    CustomerName = bid.Customer.FirstName + bid.Customer.LastName,
                },
                Status = true,
            };
        }
    }
}
