using AutomotiveEcommercePlatform.Server.Models;
using DataBase_LastTesting.Models;
using System.ComponentModel.DataAnnotations;

namespace AutomotiveEcommercePlatform.Server.DTOs.CarInfoPageDTOs
{
    public class CarInfoResponseDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string BrandName { get; set; }
        [MaxLength(50)]
        public string ModelName { get; set; }
        public int ModelYear { get; set; }
        public decimal Price { get; set; }
        [MaxLength(15)]
        public string CarCategory { get; set; }
        public string CarImage { get; set; } 
        public bool InStock { get; set; }
        public double? TraderRating { get; set; }

        [MaxLength(50)] 
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public List<CarReview> carReviews { get; set; }
    }
}
