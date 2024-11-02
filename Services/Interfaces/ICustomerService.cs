using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetCurrentCustomer();
        Task<BaseResponse<CustomerResponseModel>> Create(CustomerRequestModel request);
        Task<BaseResponse<CustomerResponseModel>> UpdateCustomer(string id, UpdateCustomerRequest request);
        Task<BaseResponse<CustomerResponseModel>> DeleteCustomer(string id);
        Task<BaseResponse<ICollection<CustomerResponseModel>>> GetAllCustomers();
        Task<BaseResponse<CustomerResponseModel>> GetCustomer(string Id);
        Task<BaseResponse<CustomerRequestModel>> GetCustomerUpdate(string Id);
        Task<BaseResponse<ICollection<AddressResponseModel>>> ViewAddresses();
        Task<BaseResponse<ICollection<PaymentResponseModel>>> ViewPayments();
        Task<BaseResponse<ICollection<AuctionResponseModel>>> ViewAuctions();
        Task<BaseResponse<ICollection<AuctionResponseModel>>> ViewOutstandingPayments();
        //Task<BaseResponse<ICollection<ShippingResponseModel>>> ViewShipping();
  Task<BaseResponse<ICollection<BidResponseModel>>> ViewBids();
        Task<BaseResponse<CustomerResponseModel>> CancelOutstanding(string id);
        Task<BaseResponse<CustomerResponseModel>> GetProfile();
    }
}
