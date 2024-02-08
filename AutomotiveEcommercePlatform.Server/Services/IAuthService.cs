using AutomotiveEcommercePlatform.Server.Models;
using AutomotiveEcommercePlatform.Server.DTOs;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public interface IAuthService
    {
        Task<AuthResult> AddRoleAsync(AddRoleModel model);
    }
}
