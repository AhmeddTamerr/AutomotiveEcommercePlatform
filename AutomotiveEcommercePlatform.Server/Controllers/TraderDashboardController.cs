using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.DTOs.TraderDashboardDTOs;
using AutomotiveEcommercePlatform.Server.Services;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using System.Text.Json;

namespace AutomotiveEcommercePlatform.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraderDashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TraderDashboardController(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{traderid}")]
        public async Task<IActionResult> GetTraderInfoAsync(string traderid)
        {
            var trader = await _userManager.FindByIdAsync(traderid);

            var Responce = new TraderInfoDTO()
            {
                TraderName = trader.DisplayName,
                PhoneNumber = trader.PhoneNumber,
                Email = trader.Email,
            };
            return Ok(Responce);
        }


        [HttpPut("{traderId}")]
        public async Task<IActionResult> EditCarAsync(string traderid , EditCarsDTO dto)
        {
            var trader = await _context.Traders.SingleOrDefaultAsync(t => t.TraderId == traderid);
            var cars = await _context.Cars.Where(c => c.TraderId == trader.TraderId);
            var car = new Car();

            if (cars == null)
                return BadRequest("NULl");
            car.Price=dto.Price;
            _context.SaveChanges();
            return Ok(car);
        }


        [HttpGet]
        public async Task<IActionResult> GetTraderCars(string traderId)
        {
            var trader = await _context.Traders.SingleOrDefaultAsync(t => t.TraderId == traderId);
            if (trader == null)
                return NotFound("Trader does not exist!");

            // var cars = _carService.GetAllTraderCars(traderId);
            var cars = await _context.Cars.Where(c => c.TraderId == trader.TraderId).ToListAsync();
            if (cars==null)
                return NotFound("No Cars For this Trader !");
            return Ok(cars);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddCarDto addCarDto)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (addCarDto == null|| addCarDto.TraderId == null)
                return BadRequest("Invalid Data !");

            var validTrader = await _userManager.FindByIdAsync(addCarDto.TraderId);

            if (validTrader == null)
                return NotFound("Trader is not exists!");

            if (!await _userManager.IsInRoleAsync(validTrader,"Trader"))
                return BadRequest("UnAuthorized Party!");

            // check for required fields 
            if (addCarDto.BrandName == null ||
                addCarDto.CarCategory == null ||
                addCarDto.CarImage == null ||
                addCarDto.ModelName == null ||
                addCarDto.ModelYear == null ||
                addCarDto.Price == null 
                )
                return BadRequest("Missing a required field !");
            if (addCarDto.Price < 0)
                return BadRequest("Price can not be negative !");
            if (DateTime.Now.Year  < addCarDto.ModelYear || addCarDto.ModelYear < 1885)
                return BadRequest("Invalid Model Year!");

            var car = new Car()
            {
                BrandName = addCarDto.BrandName,
                ModelName = addCarDto.ModelName,
                ModelYear = addCarDto.ModelYear,
                Price = addCarDto.Price,
                CarCategory = addCarDto.CarCategory,
                CarImage = addCarDto.CarImage,
                TraderId = addCarDto.TraderId,
                InStock = true
            };
            await _context.Cars.AddAsync(car);
            _context.SaveChanges();
            return Ok(car);
        }
        

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] int id)
        {
            var car = await _context.Cars.SingleOrDefaultAsync(c => c.Id == id);

            if (car == null) 
                return NotFound("This Car does not Exist!");

            _context.Remove(car);
            _context.SaveChanges();
            return Ok(car);
        }
        

    }
}
