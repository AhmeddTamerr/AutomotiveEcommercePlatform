using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.DTOs.ReviewsDTO;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;

namespace AutomotiveEcommercePlatform.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddTraderReviewAsync([FromQuery]string traderId,[FromBody]TraderReviewDTO dto)
        {

            if (dto.Rating > 5)
                return BadRequest("The Rating cant exceed 5");

            var tradingtating = new TraderRating
            {
                Rating = dto.Rating,
                UserId = dto.UserId,
                TraderId = traderId
            };
            await _context.AddAsync(tradingtating);
            _context.SaveChanges();

            return Ok(tradingtating);
        }

        [HttpPost("CarReview")]

        public async Task<IActionResult> AddCarReviewAsync([FromQuery]int carId ,[FromBody]CarReviewDTO dto)
        {
            if (dto.Rating > 5)
                return BadRequest("The Rating cant exceed 5");

            var carreview = new CarReview
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                UserId = dto.UserId,
                CarId = carId
            };

            
            await _context.AddAsync(carreview);
            _context.SaveChanges();

            return Ok(carreview);
        }
    }
}
