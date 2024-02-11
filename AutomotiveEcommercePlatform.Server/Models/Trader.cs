using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DataBase_LastTesting.Models
{
    public class Trader
    {
        //public int TraderId { get; set; }
        public string TraderId { get; set; }

        [JsonIgnore]
        public List<Car> Car { get; set; }
        [JsonIgnore]
        public ICollection<User> User { get; set; }
        [JsonIgnore]
        public List<TraderRating> TraderRatings { get; set; }
    }
}
