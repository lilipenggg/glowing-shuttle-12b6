using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using web.Services;
using web.Data.Entities;
using web.Models;

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

        public async Task CreateOrderItems(Order order)
        {
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            foreach (var item in shoppingCartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderItemId = new Guid().ToString(),
                    OrderItemOrderId = order.OrderId,
                    OrderItemQuantity = item.ShoppingCartItemAmount,
                    OrderItemProductId = item.ShoppingCartItemProductId
                };

                _ctx.Add(orderItem);
            }

            await _ctx.SaveChangesAsync();
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
            var order = new Order
            {
                OrderId = new Guid().ToString(), // Generate GUID for Id
                OrderAppliedAwardPoints = orderModel.OrderAppliedAwardPoints,
                OrderAppliedDiscount = orderModel.OrderAppliedDiscount,
                OrderDateTime = orderModel.OrderDateTime,
                OrderTotal = orderModel.OrderTotal
            };

            // Capture the items within shopping cart -- should be from OrderItem related data access methods
            await CreateOrderItems(order);

            // Capture order buyer (application user) id
            if (orderModel.OrderBuyerId != null)
            {
                order.OrderBuyerId = orderModel.OrderBuyerId;
            }
            else if (orderModel.OrderGuestBuyerId != null)
            {
                order.OrderGuestBuyerId = orderModel.OrderGuestBuyerId;
            }

            await _ctx.SaveChangesAsync();
        }

        #endregion
        
        #region BusinessUser

        public async Task<List<BusinessUser>> GetBusinessUsers()
        {
            return await (from u in _ctx.BusinessUser
                orderby u.BusinessUserId
                select u).ToListAsync();
        }

        public async Task<BusinessUser> GetBusinessUserById(string id)
        {
            return await _ctx.BusinessUser.SingleOrDefaultAsync(u => u.BusinessUserId == id);
        }
        
        #endregion
        
    }
}