using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class UpdateCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string PassWord { get; set; }
    }
}
