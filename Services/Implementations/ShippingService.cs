using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Implementations;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;

namespace FINALPROJECT.Services.Implementations
{
    public class ShippingService : IShippingService
    {

        private readonly IAuctionRepo _auctionRepo;
        private readonly ICustomerService _customerService;
        private readonly IShippingRepo _shippingRepo;
        public ShippingService(IAuctionRepo auctionRepo, ICustomerService customerService, IShippingRepo shippingRepo)
        {
            _auctionRepo = auctionRepo;
            _customerService = customerService;
            _shippingRepo = shippingRepo;
        }


        public async Task<BaseResponse<ShippingResponseModel>> Create(string paymentid,string auctionid,ShippingRequestModel request)
        {
            var customer = await _customerService.GetCurrentCustomer();
            var auction = await _auctionRepo.Get(x => x.Id == auctionid);
            var random = new Random();
            var number = random.Next(10000000,99999999);
            var shipping = new Shipping
            {
                Address = request.Address,
                Auction = auction,
                AuctionId = auction.Id,
                Customer = customer,
                CustomerId = customer.Id,
                PaymentReference = paymentid,
                TrackingNumber = "+23480" + number,
            };
            await _shippingRepo.Add(shipping);
            auction.Shipping = shipping;
            auction.ShippingId = shipping.Id;
            customer.Shippings.Add(shipping);
            await _shippingRepo.SaveAsync();
            return new BaseResponse<ShippingResponseModel>
            {
                Data = new ShippingResponseModel
                {
                    Address = new AddressResponseModel
                    {
                        City = shipping.Address.City,
                        Number = shipping.Address.Number,
                        PostalCode = shipping.Address.PostalCode,
                        State = shipping.Address.State,
                        Street = shipping.Address.Street,
                    },
                    CustomerId = shipping.CustomerId,
                    PaymentReference = shipping.PaymentReference,
                    TrackingNumber = shipping.TrackingNumber,
                },
                Status = true,
            };

        }

        public async Task<BaseResponse<ICollection<ShippingResponseModel>>> GetAllShippings()
        {
            var shippings= await _shippingRepo.GetAll();
            if (shippings == null)
            {
                return new BaseResponse<ICollection<ShippingResponseModel>>
                {
                    Status = false,
                    Message = "There are no Shippings yet"
                };
            }
            return new BaseResponse<ICollection<ShippingResponseModel>>
            {
                Status = true,
                Data = shippings.Select(shipping => new ShippingResponseModel
                {
                    Address = new AddressResponseModel
                    {
                        City = shipping.Address.City,
                        Number = shipping.Address.Number,
                        PostalCode = shipping.Address.PostalCode,
                        State = shipping.Address.State,
                        Street = shipping.Address.Street,
                    },
                    CustomerId = shipping.CustomerId,
                    PaymentReference = shipping.PaymentReference,
                    TrackingNumber = shipping.TrackingNumber,
                }).ToList(),
            };
        }

        public async Task<BaseResponse<ShippingResponseModel>> GetShipping(string Id)
        {
            var shipping  = await _shippingRepo.Get(x => x.Id == Id);
            return new BaseResponse<ShippingResponseModel>
            {
                Data = new ShippingResponseModel
                {
                     Address = new AddressResponseModel
                    {
                        City = shipping.Address.City,
                        Number = shipping.Address.Number,
                        PostalCode = shipping.Address.PostalCode,
                        State = shipping.Address.State,
                        Street = shipping.Address.Street,
                    },
                    CustomerId = shipping.CustomerId,
                    PaymentReference = shipping.PaymentReference,
                    TrackingNumber = shipping.TrackingNumber,
                },
                Status = true,
            };
        }
    }
}
