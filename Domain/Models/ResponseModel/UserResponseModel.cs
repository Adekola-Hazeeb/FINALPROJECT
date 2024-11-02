using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Models.ResponseModel
{
    public class UserResponseModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}
