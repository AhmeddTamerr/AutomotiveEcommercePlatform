using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public interface IReviewService
    {
        Task<CarReview> AddCarReview(CarReview carReview);
        Task<IEnumerable<CarReview>> GetCarReview(int id);
        Task<TraderRating> AddTraderRating(Car car);
        Task<TraderRating> GetTraderRating(int id);
    }
}
