using Newtonsoft.Json;

namespace DataBase_LastTesting.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set;}

        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public List<Car> Car { get; set; }

    }
}
