using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web.Data;
using web.Models;
using web.ViewModels;

namespace web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly KioskRepository _repository;
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartController(KioskRepository repository, ShoppingCartModel shoppingCart)
        {
            _repository = repository;
            _shoppingCart = shoppingCart;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = await _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }
    }
}