using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.DTOs;
using AutomotiveEcommercePlatform.Server.Services;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;

namespace AutomotiveEcommercePlatform.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CarController(ICarService carService , ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _carService =  carService;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("traderId")]
        public async Task<IActionResult> CreateAsync(string traderId ,[FromBody] AddCarDto addCarDto)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (addCarDto == null|| traderId==null)
                return BadRequest("Invalid Data !");

            var validTrader = await _userManager.FindByIdAsync(traderId);

            if (validTrader == null)
                return BadRequest("Trader is not exists!");

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
            if (DateTime.Now.Year < addCarDto.ModelYear || addCarDto.ModelYear < 1885)
                return BadRequest("Invalid Model Year!");

            var car = new Car()
            {
                BrandName = addCarDto.BrandName,
                ModelName = addCarDto.ModelName,
                ModelYear = addCarDto.ModelYear,
                Price = addCarDto.Price,
                CarCategory = addCarDto.CarCategory,
                CarImage = addCarDto.CarImage,
                TraderId = traderId
            };
            await _carService.Add(car);
            return Ok(car);
        }

    }
}
