using System.ComponentModel.DataAnnotations;
using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.Models;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DataBase_LastTesting.Models
{
    public class User
    {
        //public  ApplicationUser ApplicationUser { get; set; }
        public  string UserId { get; set; }

        [JsonIgnore]
        public List<Order> Order { get; set; }
        // public Trader Trader { get; set; }
        //public string TraderId { get; set; }

        [JsonIgnore]
        public List<CarReview> Review { get; set;}

        [JsonIgnore]
        public ICollection<Trader> Trader { get; set; }

        [JsonIgnore]
        public List<TraderRating> TradersRatings { get; set; }

        //public int AddressId { get; set; }

        [JsonIgnore]
        public List<Address> Addreses { get; set; }

    }
}
