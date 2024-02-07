namespace DataBase_LastTesting.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public int CarId { get; set; }
        public User User { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
