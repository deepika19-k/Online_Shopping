using Microsoft.AspNetCore.Mvc;
using OnlineShopping.MVC.Services;

namespace OnlineShopping.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // 🔥 THIS IS CALLED WHEN USER CLICKS CHECKOUT
        public async Task<IActionResult> Checkout()
        {
            int userId = 1; // temp user

            await _orderService.Checkout(userId);

            return View(); // goes to Checkout.cshtml
        }
    }
}