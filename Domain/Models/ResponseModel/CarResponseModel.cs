using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Models.ResponseModel
{
    public class CarResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string ChasisNumber { get; set; }
        public CarStatus Status { get; set; }
        public string ImageUrl { get; set; }
       
    }
}
