using AutomotiveEcommercePlatform.Server.Models;

namespace DataBase_LastTesting.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        //public int CarId { get; set; }

        //public User User { get; set; }
        public ICollection<Car> Car { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
