using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Models.ResponseModel
{
    public class ShippingResponseModel
    {
        public AddressResponseModel Address { get; set; }
        public string TrackingNumber { get; set; } 
        public string CustomerId { get; set; }
        public string PaymentReference { get; set; }
    }
}
