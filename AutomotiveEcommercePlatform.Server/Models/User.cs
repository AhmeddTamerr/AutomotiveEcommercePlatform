using System.ComponentModel.DataAnnotations;
using AutomotiveEcommercePlatform.Server.Data;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;

namespace DataBase_LastTesting.Models
{
    public class User
    {


        //public  ApplicationUser ApplicationUser { get; set; }
        public  string UserId { get; set; }
        public List<Order> Order { get; set; }
        public Trader Trader { get; set; }
        public string TraderId { get; set; }
        public List<CarReview> Review { get; set;}
        public List<TraderRating> TradersRating { get; set; }

    }
}
