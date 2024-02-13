namespace AutomotiveEcommercePlatform.Server.DTOs.ReviewsDTO
{
    public class CarReviewDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }

        public string UserId { get; set; }
        public int CarId { get; set; }
    }
}
