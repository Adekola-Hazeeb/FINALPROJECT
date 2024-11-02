namespace FINALPROJECT.Domain.Models.ResponseModel
{
    public class AddressResponseModel
    {
        public string Id {  get; set; }
        public string Number { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string? PostalCode { get; set; }
    }
}
