using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.DTOs.OrdersDTO
{
    public class OrdersDTO
    {
        // order id --> trader name , phone number , 
        public int OrderId { get; set; }
        public List<OrderCarInfoDto> CarsInfo { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public string CustomerName { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
