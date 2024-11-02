using System.ComponentModel.DataAnnotations;

namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class AddressRequestModel
    {
        [Required(ErrorMessage = "House number is required.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "PostalCode is required.")]
        public string? PostalCode { get; set; }
    }
}
