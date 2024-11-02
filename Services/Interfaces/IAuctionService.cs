using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface IAuctionService
    {
        Task<BaseResponse<AuctionResponseModel>> Create(AuctionRequestModel request);
        Task<BaseResponse<AuctionResponseModel>> UpdateAuction(string id ,AuctionRequestModel request);
        Task<BaseResponse<AuctionResponseModel>> DeleteAuction(string id);
        Task<BaseResponse<ICollection<AuctionResponseModel>>> GetAllAuctions();
        Task<BaseResponse<AuctionResponseModel>> GetAuction(string Id);
        Task<BaseResponse<AuctionResponseModel>> EndAuction();
        Task<BaseResponse<AuctionRequestModel>> GetAuctionUpdate(string Id);
        Task<BaseResponse<ICollection<BidResponseModel>>> GetBids(string Id);
    }
}
