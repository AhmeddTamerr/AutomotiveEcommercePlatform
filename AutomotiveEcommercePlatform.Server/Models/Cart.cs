using AutomotiveEcommercePlatform.Server.Models;

namespace DataBase_LastTesting.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        public int CarId { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
       // public ICollection<Car> Cars { get; set; }
       public List<CarsInTheCart> CarsInTheCart { get; set; }
    }
}
