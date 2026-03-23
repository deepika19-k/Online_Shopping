using OnlineShopping.API.Models;
using OnlineShopping.API.Data;
using OnlineShopping.API.Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
namespace OnlineShopping.API.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCart(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }


        public async Task<bool> AddToCart(int userId, int productId, int quantity)
        {
            if (quantity <= 0) return false;

            var cart = await GetCart(userId);

            // 🔍 Check if item already exists
            var item = await _context.CartItems
                .FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == productId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = cart.Id   // ✅ CRITICAL FIX
                };

                _context.CartItems.Add(newItem);  // ✅ MUST USE THIS
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCartItem(int userId, int productId, int quantity)
        {
            var cart = await GetCart(userId);

            var item = await _context.CartItems
                .FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == productId);

            if (item == null) return false;

            if (quantity <= 0)
            {
                _context.CartItems.Remove(item);  // ✅ remove from DB
            }
            else
            {
                item.Quantity = quantity;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveFromCart(int userId, int productId)
        {
            var cart = await GetCart(userId);

            var item = cart.CartItems?
                .FirstOrDefault(x => x.ProductId == productId);

            if (item == null) return false;

            cart.CartItems!.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ClearCart(int userId)
        {
            var cart = await GetCart(userId);
            cart.CartItems?.Clear();

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
