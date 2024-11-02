using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface IShippingService
    {
        Task<BaseResponse<ShippingResponseModel>> Create(string paymentid, string auctionid, ShippingRequestModel request);
        Task<BaseResponse<ICollection<ShippingResponseModel>>> GetAllShippings();
        Task<BaseResponse<ShippingResponseModel>> GetShipping(string Id);
    }
}

