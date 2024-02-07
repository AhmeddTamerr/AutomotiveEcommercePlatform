using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataBase_LastTesting.Models
{
    public class Trader
    {
        public int TraderId { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string DisplayName { get; set; }
        public List<Car> Car { get; set; }
        public List<TraderRating> TraderRating { get; set; }
    }
}
