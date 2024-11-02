using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Entities
{
    public class Customer : Auditables
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        public ICollection<Auction> AuctionsPartaken { get; set; } = new HashSet<Auction>();
        public ICollection<Bid> BidsMade { get; set; } = new HashSet<Bid>();
        public ICollection<Shipping> Shippings { get; set; } = new HashSet<Shipping>();
        public ICollection<Payment> PaymentsMade { get; set; } = new HashSet<Payment>();
        public ICollection<Auction> OutstandingPayments { get; set; } = new HashSet<Auction>();
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
