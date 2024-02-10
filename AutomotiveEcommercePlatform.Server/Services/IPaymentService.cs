using AutomotiveEcommercePlatform.Server.Models;
using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public interface IPaymentService
    {
        void InitializeCart(int UserId);
        Task<CartItem> AddToCart(CartItem cartItem);
        Task<CartItem> RemoveFromCart(CartItem cartItem); 

        // Task<CartItem> ClearCart(int CartId);

    }
}
