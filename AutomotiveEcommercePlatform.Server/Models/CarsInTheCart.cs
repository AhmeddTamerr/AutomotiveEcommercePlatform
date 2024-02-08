using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.Models
{
    public class CarsInTheCart
    {
        public String CartId { get; set; }
        public Cart Cart { get; set; }
        public int CarId { get; set;}
        public Car Car { get; set; }
    }
}
