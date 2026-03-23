using OnlineShopping.API.Models;

namespace OnlineShopping.API.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCart(int userId);
        Task<bool> AddToCart(int userId, int productId, int quantity);
        Task<bool> UpdateCartItem(int userId, int productId, int quantity);
        Task<bool> RemoveFromCart(int userId, int productId);
        Task<bool> ClearCart(int userId);
    }
}
