using OnlineShopping.API.Models;
using OnlineShopping.API.Services.Interfaces;
using OnlineShopping.API.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopping.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> PlaceOrder(int userId)
        {
            // ✅ CHECK USER EXISTS
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);

            if (!userExists)
            {
                var user = new User
                {
                    FirstName = "Test",
                    LastName = "User",
                    Email = "test@gmail.com",
                    PhoneNumber = "9999999999",
                    PasswordHash = "1234", // simple for now
                    Role = "User"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                userId = user.Id; // ✅ IMPORTANT
            }

            // 🔽 EXISTING CODE
            var cart = await _context.Carts
                .Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems!.Any())
                return false;

            var order = new Order
            {
                UserId = userId,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;

            foreach (var item in cart.CartItems)
            {
                total += item.Quantity * item.Product!.Price;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            order.TotalAmount = total;

            _context.Orders.Add(order);

            // ✅ CLEAR CART PROPERLY
            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Order>> GetOrders(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
