using System.ComponentModel.DataAnnotations;

namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class BidRequestModel
    {
        public string AuctionId { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public double Amount { get; set; }
    }
}
