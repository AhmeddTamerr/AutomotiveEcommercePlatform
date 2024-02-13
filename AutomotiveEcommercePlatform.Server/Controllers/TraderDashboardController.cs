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


        [HttpPut("{carid}")]
        public async Task<IActionResult> EditCarAsync(int carid , EditCarsDTO dto)
        {
            var car = _context.Cars.SingleOrDefault(g=>g.Id == carid);
            //if (car == null)
            //    return BadRequest("No Car was found with that Id"); sknce its gonna be onclick so its not needed
            //if (dto.Price == car.Price)
            //    return BadRequest("No changes were made");
            if (dto.CarCategory !=string.Empty){ car.CarCategory = dto.CarCategory; }
            if (dto.Price!=-1) {car.Price = dto.Price;}
            if (dto.BrandName != string.Empty){ car.BrandName = dto.BrandName;}
            if (dto.ModelName != string.Empty){ car.ModelName = dto.ModelName; }
            if (dto.ModelYear != 1) { car.ModelYear = dto.ModelYear; }
            if (dto.CarImage != string.Empty) {car.CarImage=dto.CarImage;}

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
            var cars = await _context.Cars.Where(c => c.TraderId == trader.TraderId ).Where(c=>c.InStock==true)
                .ToListAsync();
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
        public async Task<IActionResult> DeleteAsync(string traderId,int carId)
        {
            var trader = await _context.Traders.SingleOrDefaultAsync(t => t.TraderId == traderId);
            if (trader == null)
                return NotFound("Trader does not Exist!");

            var car = await _context.Cars.SingleOrDefaultAsync(c => c.Id == carId);
            if (car == null) 
                return NotFound("This Car does not Exist!");

           

            if (car.TraderId == traderId)
                return Unauthorized("This action is not allowed!");

            

            car.InStock = false;
            _context.SaveChanges();
            return Ok(car);
        }
        

    }
}
