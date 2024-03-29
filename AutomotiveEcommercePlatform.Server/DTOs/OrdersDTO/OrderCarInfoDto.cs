﻿using System.ComponentModel.DataAnnotations;

namespace AutomotiveEcommercePlatform.Server.DTOs.OrdersDTO
{
    public class OrderCarInfoDto
    {
        public int CarId { get; set; }
        [MaxLength(50)]
        public string BrandName { get; set; }
        [MaxLength(50)]
        public string ModelName { get; set; }
        public int ModelYear { get; set; }
        public decimal Price { get; set; }
        [MaxLength(15)]
        public string CarCategory { get; set; }
        public string TraderName { get; set; }
    }
}
