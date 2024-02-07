using AutomotiveEcommercePlatform.Server.Models.DTOs;
using ReactApp1.Server.Data;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
/*
        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return user;
        }
*/

    }
}
