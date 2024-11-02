namespace FINALPROJECT.Domain.Entities
{
    public class Bid : Auditables
    {
        public double Amount { get; set; }
        public string AuctionId { get; set; }
        public Auction Auction { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
