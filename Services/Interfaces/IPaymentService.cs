using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponse<PaymentResponseModel>> Create(double amount,string auctionid, string refrence);
        Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllPayments();
        Task<BaseResponse<PaymentResponseModel>> GetPayment(string Id);
        Task<BaseResponse<PaymentResponseModel>> MakePayment(string auctionid);
        //void VerifyPayment(string refrence);
    }
}
