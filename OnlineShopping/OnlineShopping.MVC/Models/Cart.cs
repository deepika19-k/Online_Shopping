namespace OnlineShopping.MVC.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
