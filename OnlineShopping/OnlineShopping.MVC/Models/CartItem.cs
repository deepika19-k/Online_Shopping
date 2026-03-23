namespace OnlineShopping.MVC.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }   // ✅ MUST
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
