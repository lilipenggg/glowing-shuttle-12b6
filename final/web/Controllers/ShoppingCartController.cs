using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web.Data;
using web.Enums;
using web.Models;
using web.Services;
using web.ViewModels;

namespace web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IKioskRepository _repository;
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartController(IKioskRepository repository, ShoppingCartModel shoppingCart)
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
                ShoppingCartTotal = await _shoppingCart.GetShoppingCartTotal(),
                TaxPercentage = new TaxPercentage().Value
            };

            return View(shoppingCartViewModel);
        }
        
        public async Task<RedirectToActionResult> AddToShoppingCart(string productId)
        {
            var selectedProduct = await _repository.GetProductById(productId);

            if (selectedProduct != null)
            {
                await _shoppingCart.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }
    }
}