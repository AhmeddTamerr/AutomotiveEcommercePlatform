﻿using AutomotiveEcommercePlatform.Server.Data;
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

            // Trader Can not remove the data of sold car 
            if (car.OrderId != null)
                return BadRequest("Removing Data of Sold Car is not allowed!");

            // Remove car Comments and Reviews before removing the car 
            var carReviews = await _context.CarReviews.Where(cr => cr.CarId == carId).ToListAsync();
            if (carReviews != null)
            {
                foreach (var review in carReviews)
                    _context.Remove(review);
                _context.SaveChanges();
            }
            // Remove the car from all carts 
            var carItems = await _context.CarReviews.Where(c => c.CarId == carId).ToListAsync();
            if (carItems != null)
            {
                foreach (var item in carItems)
                    _context.Remove(item);
                _context.SaveChanges();
            }




            _context.Remove(car);
            _context.SaveChanges();
            return Ok(car);
        }
        

    }
}
