using System.ComponentModel.DataAnnotations;
using FINALPROJECT.Domain.Entities;

namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class ShippingRequestModel
    {
        public Address Address { get; set; }
    }
}
