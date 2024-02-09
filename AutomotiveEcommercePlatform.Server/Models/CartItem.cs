using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.Models
{
    public class CartItem
    {
        public string  CartId { get; set; }
        public int   CarId { get; set; }

        public Cart Cart { get; set; }
        public Car Car { get; set; }

    }
}
