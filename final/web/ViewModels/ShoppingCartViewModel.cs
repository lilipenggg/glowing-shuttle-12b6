using web.Models;

namespace web.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartModel ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
    }
}