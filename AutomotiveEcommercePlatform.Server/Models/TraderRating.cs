using DataBase_LastTesting.Models;

namespace DataBase_LastTesting.Models
{
    public class TraderRating
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public Trader Trader { get; set; }
        public User User { get; set; }
    }
}
