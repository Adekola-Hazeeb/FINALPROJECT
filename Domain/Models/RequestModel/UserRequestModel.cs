using System.ComponentModel.DataAnnotations;

namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
