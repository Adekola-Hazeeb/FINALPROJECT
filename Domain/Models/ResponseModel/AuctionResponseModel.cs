using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Models.ResponseModel
{
    public class AuctionResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AuctionStartDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public double StartingPrice { get; set; }
        public double CurrentPrice { get; set; }
        public string ImageURL { get; set; }
        public AuctionStatus Status { get; set; }
        public string CarId { get; set; }
        public string CustomerId { get; set; }
        public ICollection<BidResponseModel> Bids { get; set; } = new HashSet<BidResponseModel>();
    }
}
