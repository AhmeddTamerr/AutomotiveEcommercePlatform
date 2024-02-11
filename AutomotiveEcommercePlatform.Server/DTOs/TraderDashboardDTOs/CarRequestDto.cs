using AutomotiveEcommercePlatform.Server.Models;
using DataBase_LastTesting.Models;
using System.ComponentModel.DataAnnotations;

namespace AutomotiveEcommercePlatform.Server.DTOs.TraderDashboardDTOs
{
    public class CarRequestDto
    {
        // This info will be modified according to filtration page (Searching Page)
        public string BrandName { get; set; }

        [MaxLength(50)]
        public string ModelName { get; set; }
        public int ModelYear { get; set; }
        public decimal StartPrice { get; set; }
        public decimal EndPrice { get; set; }

        [MaxLength(15)]
        public string CarCategory { get; set; }
    }
}
