using DataBase_LastTesting.Models;
using Newtonsoft.Json;

namespace DataBase_LastTesting.Models
{
    public class TraderRating
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        
        public string TraderId { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public Trader Trader { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
