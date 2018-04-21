using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using web.Models;
using web.Services;
using web.ViewModels;

namespace web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IKioskRepository _repository;
        private readonly ShoppingCartModel _shoppingCart;

        public OrderController(IKioskRepository repository, ShoppingCartModel shoppingCart)
        {
            _repository = repository;
            _shoppingCart = shoppingCart;
        }
        
        // GET
        public async Task<IActionResult> Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel orderModel)
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your shopping cart is empty, add something first");
            }

            if (ModelState.IsValid)
            {
                // create order and associated order items
                await _repository.CreateOrder(orderModel, items);

                await _shoppingCart.CleanCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(orderModel);
        }

        public async Task<IActionResult> CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. Your order will be processed shortly!";
            return View();
        }
    }
}