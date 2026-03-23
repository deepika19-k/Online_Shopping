using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.API.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
