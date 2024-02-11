using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public interface ICarService
    {
        Task<Car> Add(Car car);
        Car Update (Car car);
        Car Delete(Car car);
        Task<Car> GetById(int id); // for Car info page 
        Task<IEnumerable<Car>> GetAll(); // for searching page 
        Task<IEnumerable<Car>> GetAllTraderCars(string traderId); // for trader dashboard 

    }
}
