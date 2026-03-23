namespace OnlineShopping.API.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        // For image upload
        public IFormFile? Image { get; set; }
    }
}