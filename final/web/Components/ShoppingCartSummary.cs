using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Data.Entities;
using web.Models;
using web.ViewModels;
using System.Collections.Generic;

namespace web.Components
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartSummary(ShoppingCartModel shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var items = await _shoppingCart.GetShoppingCartItems();
            var items = new List<ShoppingCartItem>() {new ShoppingCartItem(), new ShoppingCartItem()};
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