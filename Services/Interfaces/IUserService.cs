using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Domain.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FINALPROJECT.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponseModel>> Login(UserRequestModel request);
        Task<User> GetCurrentUser();
    }
}
