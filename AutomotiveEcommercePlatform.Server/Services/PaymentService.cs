using AutomotiveEcommercePlatform.Server.Models;

namespace AutomotiveEcommercePlatform.Server.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<CartItem> AddToCart(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public void InitializeCart(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> RemoveFromCart(CartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
