using Microsoft.AspNetCore.Mvc;
using OnlineShopping.API.Services.Interfaces;

namespace OnlineShopping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var result = await _orderService.PlaceOrder(userId);

            if (!result)
                return BadRequest("Cart is empty");

            return Ok("Order placed successfully");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrders(int userId)
        {
            var orders = await _orderService.GetOrders(userId);
            return Ok(orders);
        }
    }
}
