using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.DTOs.CarInfoPageDTOs;
using AutomotiveEcommercePlatform.Server.DTOs.SearchDTOs;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;

namespace AutomotiveEcommercePlatform.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltrationPageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FiltrationPageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cars = _context.Cars.Where(c=>c.InStock == true);

            var carsInfo = new List<CarInfoResponseDto>();

            foreach (var car in cars)
            {
                var Trader = await _userManager.FindByIdAsync(car.TraderId);
                if (Trader == null) continue;

                var averageTraderRating = _context.TraderRatings
                    .Where(tr => tr.TraderId == car.TraderId) 
                    .Select(tr => tr.Rating)
                    .ToList()
                    .DefaultIfEmpty(0) 
                    .Average();

                var responce = new CarInfoResponseDto()
                {
                    Id = car.Id,
                    ModelName = car.ModelName,
                    BrandName = car.BrandName,
                    CarCategory = car.CarCategory,
                    CarImage =car.CarImage,
                    ModelYear = car.ModelYear,
                    Price = car.Price,
                    carReviews = car.CarReviews,
                    FirstName = Trader.FirstName,
                    LastName = Trader.LastName,
                    PhoneNumber = Trader.PhoneNumber,
                    InStock = car.InStock,
                    TraderRating = averageTraderRating
                };
                carsInfo.Add(responce);
            }

            return Ok(carsInfo);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetFilteredCars([FromQuery]SearchDto searchDto)
        {
            if (searchDto == null)
                return NotFound("Not Found the Page !");

            IQueryable<Car> query = _context.Cars.Where(c=>c.InStock==true);

            if (!string.IsNullOrEmpty(searchDto.BrandName))
                query = query.Where(q => q.BrandName.ToUpper() == searchDto.BrandName.ToUpper());

            if (!string.IsNullOrEmpty(searchDto.CarCategory))
                query = query.Where(q => q.CarCategory.ToUpper() == searchDto.CarCategory.ToUpper());

            if (!string.IsNullOrEmpty(searchDto.ModelName))
                query = query.Where(q => q.ModelName.ToUpper() == searchDto.ModelName.ToUpper());

            if (searchDto.ModelYear!=null)
                query = query.Where(q => q.ModelYear == searchDto.ModelYear);

            if (searchDto.minPrice != null)
                query = query.Where(q => q.Price >= searchDto.minPrice);

            if (searchDto.maxPrice != null)
                query = query.Where(q => q.Price <= searchDto.maxPrice);


            return Ok (query);

        }   
    }
}
