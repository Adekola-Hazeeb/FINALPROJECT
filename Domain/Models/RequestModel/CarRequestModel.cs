using System.ComponentModel.DataAnnotations;

namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class CarRequestModel
    {
        [Required(ErrorMessage = "Car name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(30, ErrorMessage = "Color cannot exceed 30 characters.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Chassis number is required.")]
        [StringLength(20, ErrorMessage = "Chassis number cannot exceed 20 characters.")]
        public string ChasisNumber { get; set; }

        [Required(ErrorMessage = "Car image is required.")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
