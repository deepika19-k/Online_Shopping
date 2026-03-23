using Microsoft.AspNetCore.Mvc;
using OnlineShopping.MVC.Services;  
namespace OnlineShopping.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1;

            var items = await _cartService.GetCart(userId);

            return View(items); // ✅ IMPORTANT
        }

        public async Task<IActionResult> Add(int productId)
        {
            int userId = 1;

            await _cartService.AddToCart(userId, productId, 1);
            return RedirectToAction("Index");
        }
    }
}
