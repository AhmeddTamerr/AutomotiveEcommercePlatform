using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.DTOs.OrdersDTO;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;

namespace AutomotiveEcommercePlatform.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet ("{OrderId}")]
        public async Task<IActionResult> GetOrderDetailsAsync(int OrderId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(c => c.Id == OrderId);
            var user = await _userManager.FindByIdAsync(order.UserId);
            var cart = await _context.CartItems.SingleOrDefaultAsync(c => c.CartId == order.UserId);
            var car = await _context.Cars.SingleOrDefaultAsync(c=>c.Id == cart.CarId);
            var trader = await _userManager.FindByIdAsync(car.TraderId);


            var Responce = new OrdersDTO()
            {
                OrderId = OrderId,
                CustomerName = user.DisplayName,
                CarDetails = car.BrandName + car.ModelName + car.ModelYear,
                TraderName = trader.DisplayName,
                Price = order.Price,
                PaymentStatus = "Completed",
                PurchaseDate = order.PurchaseDate,
            };
            return Ok(Responce);
        }
    }
}
