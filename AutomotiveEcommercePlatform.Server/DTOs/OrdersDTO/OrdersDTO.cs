using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.DTOs.OrdersDTO
{
    public class OrdersDTO
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CarDetails { get; set; }
        public string TraderName { get; set; }
        public decimal Price { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
