using OnlineShopping.API.Models;

namespace OnlineShopping.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder(int userId);
        Task<List<Order>> GetOrders(int userId);
    }
}

