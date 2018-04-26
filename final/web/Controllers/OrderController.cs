using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;
using web.Data.Entities;
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
                // redirect back to the shopping cart index page
                ModelState.AddModelError("", "Your shopping cart is empty, add something first");
                return RedirectToAction("Index", "ShoppingCart");
            }

            if (ModelState.IsValid)
            {
                // create order and associated order items
                await _repository.CreateOrder(orderModel, items, null);

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
        
        /// <summary>
        /// This controller directs user to a checkout view that is specific to customers who have signed in
        /// and it provides the ability for signed in customers to apply reward points during checkout
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CheckoutSignedIn()
        {
            var user = await _repository.GetApplicationUserByUserName(User.Identity.Name);
            var model = new OrderCheckoutViewModel
            {
                Order = new OrderModel(),
                ApplicationUser = new ApplicationUserModel
                {
                    ApplicationUserFirstName = user.ApplicationUserFirstName,
                    ApplicationUserLastName = user.ApplicationUserLastName,
                    ApplicatinUserUserName = user.UserName,
                    ApplicationUserAwardPoints = user.ApplicationUserAwardPoints,
                    ApplicationUserEmail = user.ApplicationUserEmail,
                    ApplicationUserPhoneNumber = user.ApplicationUserPhoneNumber
                }
            };
            return View(model);
        }

        /// <summary>
        /// This controller is responsible for signed in user to check out since loyalty feature is available for signed in user
        /// </summary>
        /// <param name="orderCheckoutViewModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CheckoutSignedIn(OrderCheckoutViewModel orderCheckoutViewModel)
        {
            var orderModel = orderCheckoutViewModel.Order;
            
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var totalBeforeTax = await _shoppingCart.GetShoppingCartTotalBeforeTax();

            var user = await _repository.GetApplicationUserByUserName(User.Identity.Name);

            // Make sure that the shopping cart is not empty when checking out
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your shopping cart is empty, add something first");
                return RedirectToAction("Index", "ShoppingCart");
            }

            // Make sure that user is not able to apply more award points than what they have
            if (orderModel.OrderAppliedAwardPoints > user.ApplicationUserAwardPoints)
            {
                ModelState.AddModelError("", "Your applied award points is more than what you currently have, adjust it to a smaller amount first");
            }

            // Make sure that the user cannot use award points credit that exceed the order total before tax
            if (orderModel.OrderAppliedAwardPoints / 30 * 10 > totalBeforeTax)
            {
                ModelState.AddModelError("", "The maximum credit cannot exceed the order total before tax");
            }

            if (ModelState.IsValid)
            {
                // Create order and associated order items
                await _repository.CreateOrder(orderModel, items, User.Identity.Name);

                await _shoppingCart.CleanCart();
                return RedirectToAction("CheckoutComplete");
            }

            // Need to create a new application user model object so that the returning model won't encounter null pointer exception
            orderCheckoutViewModel.ApplicationUser = new ApplicationUserModel
            {
                ApplicationUserAwardPoints = user.ApplicationUserAwardPoints,
                ApplicatinUserUserName = user.UserName,
                ApplicationUserEmail = user.Email,
                ApplicationUserFirstName = user.ApplicationUserFirstName,
                ApplicationUserLastName = user.ApplicationUserLastName,
                ApplicationUserPhoneNumber = user.ApplicationUserPhoneNumber
            };
            
            return View(orderCheckoutViewModel);
        }
    }
}