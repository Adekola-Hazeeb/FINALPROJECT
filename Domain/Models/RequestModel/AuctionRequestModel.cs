using System.ComponentModel.DataAnnotations;

namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class AuctionRequestModel
    {
        [Required(ErrorMessage = "Car ID is required.")]
        public string CarId { get; set; }

        [Required(ErrorMessage = "Auction end date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime AuctionEndDate { get; set; }

        [Required(ErrorMessage = "Starting price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting price must be greater than zero.")]
        public double StartingPrice { get; set; }

    }
}
