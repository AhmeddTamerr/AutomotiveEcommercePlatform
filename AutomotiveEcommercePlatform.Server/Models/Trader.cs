using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataBase_LastTesting.Models
{
    public class Trader
    {
        //public int TraderId { get; set; }
        public string TraderId { get; set; }
        public List<Car> Car { get; set; }

        public ICollection<User> User { get; set; }
        public List<TraderRating> TraderRatings { get; set; }
    }
}
