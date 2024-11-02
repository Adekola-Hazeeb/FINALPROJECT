using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Models.ResponseModel
{
    public class CustomerResponseModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
    }
}
