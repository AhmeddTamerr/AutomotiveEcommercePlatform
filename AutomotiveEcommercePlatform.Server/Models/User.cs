using System.ComponentModel.DataAnnotations;
using AutomotiveEcommercePlatform.Server.Data;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;

namespace DataBase_LastTesting.Models
{
    public class User
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string DisplayName { get; set; }

        //public  ApplicationUser ApplicationUser { get; set; }
        public  string ApplicationUserId { get; set; }

        public List<Order> Order { get; set; }
        public Trader Trader { get; set; }
        public int? TraderId { get; set; }
        public List<CarReview> Review { get; set;}
        public List<TraderRating> TradersRating { get; set; }
        public Cart Cart { get; set; }
    }
}
