namespace OnlineShopping.API.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        // Optional image update
        public IFormFile? Image { get; set; }
    }
}