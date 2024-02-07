namespace DataBase_LastTesting.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set;}
        public User User { get; set; }
        public List<Car> Car { get; set; }

    }
}
