using OnlineShopping.MVC.Models;

namespace OnlineShopping.MVC.Services
{
    public class CartService
    {
        private readonly HttpClient _http;

        public CartService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        public async Task AddToCart(int userId, int productId, int quantity)
        {
            await _http.PostAsync(
                $"api/cart/add?userId={userId}&productId={productId}&quantity={quantity}", null);
        }

        public async Task<List<CartViewModel>> GetCart(int userId)
        {
            var cart = await _http.GetFromJsonAsync<Cart>($"api/cart/{userId}");

            return cart.CartItems.Select(x => new CartViewModel
            {
                ProductName = x.Product.Name,
                Price = x.Product.Price,
                Quantity = x.Quantity,
                ImageUrl = x.Product.ImageUrl   // ✅ ADD THIS

            }).ToList();
        }
    }
}
