using System.ComponentModel.DataAnnotations;

namespace AutomotiveEcommercePlatform.Server.DTOs.SearchDTOs
{
    public class SearchDto
    {
        [MaxLength(50)]
        public string BrandName { get; set; } = string.Empty;

        [MaxLength(50)] public string ModelName { get; set; } = string.Empty;
        public int? ModelYear { get; set; } = null;
        public decimal? minPrice { get; set; } = null;
        public decimal? maxPrice { get; set; } = null;
        [MaxLength(15)] public string CarCategory { get; set; } = string.Empty;
    }
}
