using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.API.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
