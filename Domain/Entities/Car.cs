using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Entities
{
    public class Car : Auditables
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string ChasisNumber { get; set; }
        public CarStatus Status { get; set; }
        public string ImageUrl  { get; set; }
        public ICollection<Auction> Auctions { get; set; } = new HashSet<Auction>();



    }
}
