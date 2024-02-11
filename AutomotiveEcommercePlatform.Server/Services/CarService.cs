using DataBase_LastTesting.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.Remove(car);
            _context.SaveChanges();
            return car;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> GetAllTraderCars(string traderId)
        {
            return await _context.Cars
                .Where(c => c.TraderId == traderId)
                .ToListAsync();
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
