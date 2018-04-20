using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using web.Services;
using web.Data.Entities;
using web.Models;
using web.ViewModels;

namespace web.Data
{
    public class KioskRepository : IKioskRepository
    {
        private readonly KioskContext _ctx;
        private readonly ShoppingCartModel _shoppingCart;

        public KioskRepository(KioskContext ctx, ShoppingCartModel shoppingCart)
        {
            _ctx = ctx;
            _shoppingCart = shoppingCart;
        }

        #region Product
        
        public async Task<List<Product>> GetProducts()
        {
            return await _ctx.Product.OrderBy(p => p.ProductName).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _ctx.Product.Include(p => p.ProductCategory).SingleOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<List<Product>> GetProductByCategory(string category)
        {
            return await (from p in _ctx.Product
                    where p.ProductCategory.CategoryName == category
                    select p)
                .Include(x => x.ProductCategory)
                .ToListAsync();
        }

        #endregion

        #region Category

        public async Task<List<Category>> GetCategories()
        {
            return await _ctx.Category.OrderBy(c => c.CategoryName).ToListAsync();
        }

        public async Task<Category> GetCategoryById(string categoryId)
        {
            return await _ctx.Category.SingleOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _ctx.Category.SingleOrDefaultAsync(c => c.CategoryName == categoryName);
        }

        #endregion
        
        #region ShoppingCartItem

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems(string cartId)
        {
            return await (from s in _ctx.ShoppingCartItem
                where s.ShoppingCartId == cartId
                select s).ToListAsync();
        }
        
        #endregion
        
        #region OrderItem

        public async Task<List<OrderItem>> GetOrderItemsById(string orderId)
        {
            return await _ctx.OrderItem.Include(oi => oi.OrderItemOrderId == orderId).ToListAsync();
        }
        
        #endregion

        #region Order
        
        public async Task<List<Order>> GetOrders()
        {
            return await (from o in _ctx.Order
                orderby o.OrderId
                select o).ToListAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _ctx.Order.Include(o => o.OrderItem).SingleOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task CreateOrder(OrderModel orderModel)
        {
            string orderId = new Guid().ToString();
            var shoppingCartItems = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = shoppingCartItems;
            
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in shoppingCartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderItemId = new Guid().ToString(),
                    OrderItemOrderId = orderId,
                    OrderItemQuantity = item.ShoppingCartItemAmount,
                    OrderItemProductId = item.ShoppingCartItemProductId
                };

                orderItems.Add(orderItem);
            }

            var orderTotal = await _shoppingCart.GetShoppingCartTotal();
            
            _ctx.Add(new Order
            {
                OrderId = orderId,
                OrderDateTime = DateTime.Now,
                OrderAppliedAwardPoints = orderModel.OrderAppliedAwardPoints,
                OrderAppliedDiscount = orderModel.OrderAppliedDiscount,
                OrderBillingAddress1 = orderModel.OrderBillingAddress1,
                OrderBillingAddress2 = orderModel.OrderBillingAddress2,
                OrderBillingCity = orderModel.OrderBillingCity,
                OrderBillingFirstName = orderModel.OrderBillingFirstName,
                OrderBillingLastName = orderModel.OrderBillingLastName,
                OrderBillingState = orderModel.OrderBillingState,
                OrderBillingZipCode = orderModel.OrderBillingZipCode,
                OrderCreditCard = new CreditCard
                {
                    CreditCardCvv = orderModel.OrderCreditCard.CreditCardCvv,
                    CreditCardExpirationDate = orderModel.OrderCreditCard.CreditCardExpirationDate,
                    CreditCardFirstName = orderModel.OrderCreditCard.CreditCardFirstName,
                    CreditCardLastName = orderModel.OrderCreditCard.CreditCardLastName,
                    CreditCardId = new Guid().ToString(),
                    CreditCardNumber = orderModel.OrderCreditCard.CreditCardNumber
                },
                OrderShippingAddress1 = orderModel.OrderShippingAddress1,
                OrderShippingAddress2 = orderModel.OrderShippingAddress2,
                OrderShippingCity = orderModel.OrderShippingCity,
                OrderShippingFirstName = orderModel.OrderShippingFirstName,
                OrderShippingLastName = orderModel.OrderShippingLastName,
                OrderShippingState = orderModel.OrderShippingState,
                OrderShippingZipCode = orderModel.OrderShippingZipCode,
                OrderItem = orderItems,
                OrderTotal = orderTotal
            });

            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region ApplicationUser

        public async Task<ApplicationUser> GetApplicationUserByEmail(string email)
        {
            return await _ctx.ApplicationUser.SingleOrDefaultAsync(a => a.ApplicationUserEmail == email);
        }

        public async Task<List<ApplicationUser>> GetApplicationUsers()
        {
            return await _ctx.ApplicationUser.OrderBy(a => a.ApplicationUserId).ToListAsync();
        }

        #endregion

        #region CreditCard

        public async Task<CreditCard> GetCreditCardByCardNumber(string cardNumber)
        {
            return await _ctx.CreditCard.SingleOrDefaultAsync(c => c.CreditCardNumber == cardNumber);
        }

        #endregion
        
    }
}