using AutomotiveEcommercePlatform.Server.Data;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using System.Diagnostics;
using System.Linq;
using AutomotiveEcommercePlatform.Server.DTOs.CarInfoPageDTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO.Pipelines;
using System.Collections.Generic;


namespace AutomotiveEcommercePlatform.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarInfo(int carId)
        {
            var car = await _context.Cars.SingleOrDefaultAsync(t => t.Id == carId);
            if (car == null)
                return NotFound("Car is not Found!");
            var trader = await _userManager.FindByIdAsync(car.TraderId);
            if (trader == null)
                return BadRequest("SomeThing Went Wrong !");
            // Car info + Car Review + Trader display + Trader Rating 
            var carReviews = await _context.CarReviews.Where(c => c.CarId == carId).ToListAsync();

            // var traderRating = await _context?.TraderRatings?.Where(c => c.TraderId == trader.Id)?.Select(t => t.Rating)?.AverageAsync();
            var traderRatings = _context.TraderRatings.Where(c => c.TraderId == trader.Id );
            double avgTraderRating = 0 , n = 0;
            if (traderRatings.Any())
                foreach (var rate in traderRatings)
                {
                    avgTraderRating += rate.Rating; n++;
                }
            avgTraderRating /= n;

            var reviewList = new List<CarReviewDto>();
            if (carReviews.Any())
                foreach (var review in carReviews)
                {
                    var carReview = new CarReviewDto()
                    {
                        Rating = review.Rating,
                        Comment = review.Comment
                    };
                    reviewList.Add(carReview);
                }

            var responce = new CarInfoResponseDto()
            {
                Id = carId,
                BrandName = car.BrandName,
                ModelName = car.ModelName,
                ModelYear = car.ModelYear,
                Price = car.Price,
                CarCategory = car.CarCategory,
                CarImage = car.CarImage,
                InStock = car.InStock,
                carReview = reviewList,
                TraderRating = avgTraderRating,
                FirstName = trader.FirstName,
                LastName = trader.LastName,
                PhoneNumber = trader.PhoneNumber
            };

            return Ok(responce);
        }


    }
}
