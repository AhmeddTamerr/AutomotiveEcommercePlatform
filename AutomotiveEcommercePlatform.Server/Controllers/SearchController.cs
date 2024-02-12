using AutomotiveEcommercePlatform.Server.Data;
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
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cars = await _context.Cars.ToListAsync();
            if (cars == null)
                return NotFound("No Cars Found !");
            return Ok(cars);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> GetFilteredCars([FromQuery]SearchDto searchDto)
        {
            if (searchDto == null)
                return NotFound("Not Found the Page !");

            IQueryable<Car> query = _context.Cars;

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


            return Ok (query.ToListAsync());

        }   
    }
}
