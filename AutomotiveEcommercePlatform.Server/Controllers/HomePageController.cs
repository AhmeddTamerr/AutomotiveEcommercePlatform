using AutomotiveEcommercePlatform.Server.Data;
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
    public class HomePageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomePageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

    //    [HttpGet]

    //    public async Task<IActionResult> GetTheMostExpensiveCarsAsync()
    //    {
    //        var car = new List<Car>().Where(c=> new 
    //        {
                 
    //        })
                

    //         car = await _context.Cars
    //            .Where(c => c.InStock == true)
    //            .ToListAsync();
    //        return Ok(car);
    //    }
    //}
}
