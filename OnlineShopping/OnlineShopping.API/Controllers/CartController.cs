using Microsoft.AspNetCore.Mvc;
using OnlineShopping.API.Services.Interfaces;

namespace OnlineShopping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCart(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than 0");

            var result = await _cartService.AddToCart(userId, productId, quantity);
            return result ? Ok("Added to cart") : BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int userId, int productId, int quantity)
        {
            var result = await _cartService.UpdateCartItem(userId, productId, quantity);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(int userId, int productId)
        {
            var result = await _cartService.RemoveFromCart(userId, productId);
            return result ? Ok() : NotFound();
        }
    }
}
