using System.Security.Claims;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FINALPROJECT.Services.Implementations
{
    public class UserService :IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICustomerRepo _customerRepo;
        public UserService(IUserRepo userRepo, IHttpContextAccessor contextAccessor,  ICustomerRepo customerRepo)
        {
            _userRepo = userRepo;
            _contextAccessor = contextAccessor;
            _customerRepo = customerRepo;
        }
        public int otp;
        public string _email;
        public async Task<User> GetCurrentUser()
        {
            var userId =   _contextAccessor.HttpContext.User.Claims
         .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepo.Get(a => a.Id == userId);
            return user;
        }

        public async Task<BaseResponse<UserResponseModel>> Login(UserRequestModel request)
        {
           var check = await _userRepo.Get(a => a.Email == request.Email);
            if (check != null)
            {
                var password = BCrypt.Net.BCrypt.Verify(request.Password, check.PasswordHash);
                if (password)
                {
                    return new BaseResponse<UserResponseModel>
                    {
                        Status = true,
                        Message = "Login Successfull",
                        Data = new UserResponseModel
                        {
                            Email = check.Email,
                            Role = check.Role,
                            Id = check.Id,
                        }
                    };
                }
                else
                {
                    return new BaseResponse<UserResponseModel>
                    {
                        Status = false,
                        Message = "Invalid Login Details",
                        Data = null,
                    };
                }
            }
            return new BaseResponse<UserResponseModel>
            {
                Status = false,
                Message = "Invalid Credential",
                Data = null,
            };
        }
        public int OtpGenerator()
        {
            var rand = new Random();
             var num = rand.Next(1000, 9999);
            return num;
        }
    }
}
