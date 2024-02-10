using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public class ReviewService : IReviewService
    {
        public Task<CarReview> AddCarReview(CarReview carReview)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarReview>> GetCarReview(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TraderRating> AddTraderRating(Car car)
        {
            throw new NotImplementedException();
        }

        public Task<TraderRating> GetTraderRating(int id)
        {
            throw new NotImplementedException();
        }
    }
}
