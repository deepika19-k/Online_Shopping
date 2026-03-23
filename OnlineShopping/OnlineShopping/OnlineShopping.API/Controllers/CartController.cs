using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.API.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
