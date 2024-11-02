using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Models.ResponseModel
{
    public class PaymentResponseModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string ReferenceID { get; set; }
        public string CustomerId { get; set; }
        public string AuctionId { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
