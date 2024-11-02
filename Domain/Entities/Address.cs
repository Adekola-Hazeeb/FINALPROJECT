namespace FINALPROJECT.Domain.Entities
{
    public class Address : Auditables
    {
        public string Number { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string? PostalCode { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
    }
}
