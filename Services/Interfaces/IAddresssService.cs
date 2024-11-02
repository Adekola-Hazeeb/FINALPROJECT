using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface IAddresssService
    {
        Task<BaseResponse<AddressResponseModel>> Create(AddressRequestModel request);
        Task<BaseResponse<AddressResponseModel>> UpdateAddress(string id ,AddressRequestModel request);
        Task<BaseResponse<AddressResponseModel>> DeleteAddress(string id);
        Task<BaseResponse<ICollection<AddressResponseModel>>> GetAllAddresss();
        Task<BaseResponse<AddressResponseModel>> GetAddress(string Id);
    }
}
