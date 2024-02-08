using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.Models;
using AutomotiveEcommercePlatform.Server.DTOs;
using Microsoft.AspNetCore.Identity;
using ReactApp1.Server.Data;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<AuthResult> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (model.role.ToUpper() == "ADMIN")
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string> {
                        "This Action is forbidden !"
                    }
                };
            if (user == null || !await _roleManager.RoleExistsAsync(model.role))
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string> {
                        "Invalid User Id or Role "
                    }
                };
            if (await _userManager.IsInRoleAsync(user, model.role))
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string> {
                        "User already assigned to this role"
                    }
                };
            var result = await _userManager.AddToRoleAsync(user, model.role);
            if (result.Succeeded)
                return new AuthResult()
                {
                    Result = true,
                };
            else return new AuthResult()
            {
                Result = false,
                Errors = new List<string> {
                    "Something went wrong! Please Try Again. "
                }
            };
        }
    }
}
