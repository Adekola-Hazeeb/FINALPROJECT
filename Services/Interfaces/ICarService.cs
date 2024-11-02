using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface ICarService
    {
        Task<BaseResponse<CarResponseModel>> Create(CarRequestModel request);
        Task<BaseResponse<CarResponseModel>> UpdateCar(string id ,CarRequestModel request);
        Task<BaseResponse<CarResponseModel>> DeleteCar(string id);
        Task<BaseResponse<ICollection<CarResponseModel>>> GetAllCars();
        Task<BaseResponse<CarResponseModel>> GetCarById(string Id);
        Task<BaseResponse<ICollection<CarResponseModel>>> GetCarByName(string input);
        Task<BaseResponse<CarRequestModel>> GetCarByIdUpdate(string Id);
    }
}

