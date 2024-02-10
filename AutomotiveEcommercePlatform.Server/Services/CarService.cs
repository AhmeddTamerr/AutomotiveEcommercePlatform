using DataBase_LastTesting.Models;
using ReactApp1.Server.Data;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Car> Add(Car car)
        {
            await _context.Cars.AddAsync(car);
            _context.SaveChanges();
            return car;
        }

        public Car Delete(Car car)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Car>> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Car>> GetAllTraderCars(string traderId)
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Car Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
