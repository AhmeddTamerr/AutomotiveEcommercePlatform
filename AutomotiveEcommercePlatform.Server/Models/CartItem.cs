using DataBase_LastTesting.Models;
using Newtonsoft.Json;

namespace AutomotiveEcommercePlatform.Server.Models
{
    public class CartItem
    {
        public string  CartId { get; set; }
        public int   CarId { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
        [JsonIgnore]
        public Car Car { get; set; }

    }
}
