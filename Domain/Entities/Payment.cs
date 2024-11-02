using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Entities
{
    public class Payment : Auditables
    {
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string ReferenceID { get; set; } 
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string AuctionId { get; set; }
        public Auction Auction {  get; set; }
        public string? ShippingId {  get; set; }

    }
}
