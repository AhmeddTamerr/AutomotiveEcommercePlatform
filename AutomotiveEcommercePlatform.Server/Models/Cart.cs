using AutomotiveEcommercePlatform.Server.Models;
using Newtonsoft.Json;

namespace DataBase_LastTesting.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        //public int CarId { get; set; }

        //public User User { get; set; }

        [JsonIgnore]
        public ICollection<Car> Car { get; set; }
        [JsonIgnore]
        public List<CartItem> CartItems { get; set; }
    }
}
