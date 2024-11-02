using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface IBidService
    {
        Task<BaseResponse<BidResponseModel>> Create(BidRequestModel request);
        Task<BaseResponse<ICollection<BidResponseModel>>> GetAllBids();
        Task<BaseResponse<BidResponseModel>> GetBid(string Id);
        Task<BaseResponse<ICollection<BidResponseModel>>> GetAllBidsForAuction(string id);
    }
}
