using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Entities
{
    public class Auction : Auditables
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AuctionStartDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public double StartingPrice { get; set; }
        public double CurrentPrice { get; set; }
        public string ImageURL { get; set; }
        //public AuctionStatus Status { get; set; }
        public string CarId { get; set; }
        public Car Car{ get; set; }
        public Payment? Payment { get; set; }
        public string? PaymentId { get; set; }  
        public string? ShippingId { get; set; }
        public Shipping? Shipping { get; set; }
        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();



    }
}
