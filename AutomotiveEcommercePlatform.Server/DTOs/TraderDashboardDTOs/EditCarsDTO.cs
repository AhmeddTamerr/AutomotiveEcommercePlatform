using System.ComponentModel.DataAnnotations;

namespace AutomotiveEcommercePlatform.Server.DTOs.TraderDashboardDTOs
{
    public class EditCarsDTO
    {
        public string BrandName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string ModelName { get; set; } = string.Empty;

        public int ModelYear { get; set; } = 1;
        public decimal Price { get; set; } = -1;
        [MaxLength(15)]
        public string CarCategory { get; set;} = string.Empty;

        public string CarImage { get; set; } = string.Empty;

    }
}
