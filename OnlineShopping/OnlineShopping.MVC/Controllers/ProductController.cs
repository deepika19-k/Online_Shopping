using Microsoft.AspNetCore.Mvc;
using OnlineShopping.MVC.Services;
using OnlineShopping.MVC.Models;

namespace OnlineShopping.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApiService _apiService;

        public ProductController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            string url = "/api/products";

            var query = new List<string>();

            if (categoryId.HasValue) query.Add($"categoryId={categoryId.Value}");
            if (minPrice.HasValue) query.Add($"minPrice={minPrice.Value}");
            if (maxPrice.HasValue) query.Add($"maxPrice={maxPrice.Value}");

            if (query.Any())
                url += "?" + string.Join("&", query);

            var products = await _apiService.GetAsync<List<ProductViewModel>>(url);

            return View(products);
        }

        // Create Product Page (optional if needed)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            await _apiService.PostAsync("/api/products", model);
            return RedirectToAction("Index");
        }
    }
}