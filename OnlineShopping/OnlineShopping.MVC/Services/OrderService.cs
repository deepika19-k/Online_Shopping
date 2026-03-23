using OnlineShopping.MVC.Models;
namespace OnlineShopping.MVC.Services
{
    public class OrderService
    {
        private readonly HttpClient _http;

        public OrderService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        public async Task Checkout(int userId)
        {
            var response = await _http.PostAsync($"api/orders/checkout?userId={userId}", null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Checkout failed");
            }
        }

        public async Task<List<OrderViewModel>> GetOrders(int userId)
        {
            return await _http.GetFromJsonAsync<List<OrderViewModel>>($"api/orders/{userId}");
        }
    }
}
