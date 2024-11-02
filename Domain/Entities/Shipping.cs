using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Entities
{
    public class Shipping : Auditables
    {
        public Address Address { get; set; }
        public string TrackingNumber { get; set; } 
        public string CustomerId { get; set; } 
        public Customer Customer { get; set; }
        public string AuctionId { get; set; }
        public Auction Auction { get; set; }
        public string PaymentReference { get; set; }
    }
}
