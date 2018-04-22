using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using web.Services;
using web.Data.Entities;
using web.Enums;
using web.Models;
using web.ViewModels;

namespace web.Data
{
    public class KioskRepository : IKioskRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ShoppingCartModel _shoppingCart;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public KioskRepository(ApplicationDbContext ctx, ShoppingCartModel shoppingCart, 
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _shoppingCart = shoppingCart;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #region ApplicationUser

        public async Task<ApplicationUser> GetClaimsPrincipalApplicationUser(ClaimsPrincipal userClaimsPrincipal)
        {
            return await _userManager.GetUserAsync(userClaimsPrincipal);
        }     

        #endregion

        #region Product
        
        public async Task<List<Product>> GetProducts()
        {
            return await _ctx.Product.OrderBy(p => p.ProductName).ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _ctx.Product.Include(p => p.ProductCategory).SingleOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<Product>> GetProductByCategory(string category)
        {
            return await (from p in _ctx.Product
                    where p.ProductCategory.CategoryName == category
                    select p)
                .Include(x => x.ProductCategory)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductByVendorId(string vendorId)
        {
            return await _ctx.Product.Where(p => p.ProductVendorId == vendorId).ToListAsync();
        }

        public async Task<QueryResult> CreateProduct(ProductModel productModel)
        {
            try
            {
                _ctx.Add(new Product
                {
                    ProductCategoryId = productModel.ProductCategory.CategoryId,
                    ProductDescription = productModel.ProductDescription,
                    ProductExpirationDate = productModel.ProductExpirationDate,
                    ProductImage = productModel.ProductImage,
                    ProductName = productModel.ProductName,
                    ProductQuantity = productModel.ProductQuantity,
                    ProductUnitPrice = productModel.ProductUnitPrice,
                    ProductVendorId = productModel.ProductVendorId
                });
                await _ctx.SaveChangesAsync();
                return QueryResult.Succeed;
            }
            catch (Exception e)
            {
                return QueryResult.Failed;
            }   
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

        public async Task CreateOrderItems(List<ShoppingCartItem> shoppingCartItems, Order order)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in shoppingCartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderItemId = Guid.NewGuid().ToString(),
                    OrderItemOrder = order,
                    OrderItemQuantity = item.ShoppingCartItemAmount,
                    OrderItemProductId = item.ShoppingCartItemProductId
                };

                orderItems.Add(orderItem);
            }
            
            _ctx.AddRange(orderItems);
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

        public async Task CreateOrder(OrderModel orderModel, List<ShoppingCartItem> shoppingCartItems)
        {
            var orderTotal = await _shoppingCart.GetShoppingCartTotal();
            
            // create credit card entry
            var creditCard = await CreateCreditCard(orderModel.OrderCreditCard.CreditCardCvv,
                orderModel.OrderCreditCard.CreditCardExpirationDate,
                orderModel.OrderCreditCard.CreditCardFirstName, orderModel.OrderCreditCard.CreditCardLastName,
                orderModel.OrderCreditCard.CreditCardNumber);
            
            // create order
            Order order = new Order
            {
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
                OrderCreditCard = creditCard,
                OrderShippingAddress1 = orderModel.OrderShippingAddress1,
                OrderShippingAddress2 = orderModel.OrderShippingAddress2,
                OrderShippingCity = orderModel.OrderShippingCity,
                OrderShippingFirstName = orderModel.OrderShippingFirstName,
                OrderShippingLastName = orderModel.OrderShippingLastName,
                OrderShippingState = orderModel.OrderShippingState,
                OrderShippingZipCode = orderModel.OrderShippingZipCode,
                OrderTotal = orderTotal
            };

            _ctx.Order.Add(order);
            await _ctx.SaveChangesAsync();
            
            // create assoicated order items
            await CreateOrderItems(shoppingCartItems, order);
        }

        #endregion

        #region ApplicationUser

        public async Task<ApplicationUser> GetApplicationUserByEmail(string email)
        {
            return await _ctx.ApplicationUser.SingleOrDefaultAsync(a => a.ApplicationUserEmail == email);
        }

        #endregion

        #region CreditCard

        public async Task<CreditCard> GetCreditCardById(string id)
        {
            return await _ctx.CreditCard.SingleOrDefaultAsync(c => c.CreditCardId == id);
        }

        public async Task<CreditCard> GetCreditCardByCardNumber(string cardNumber)
        {
            return await _ctx.CreditCard.SingleOrDefaultAsync(c => c.CreditCardNumber == cardNumber);
        }

        public async Task<CreditCard> CreateCreditCard(int cvv, DateTime expirationDate, string firstName, string lastName, string cardNumber)
        {
            CreditCard creditCard = new CreditCard
            {
                CreditCardCvv = cvv,
                CreditCardExpirationDate = expirationDate,
                CreditCardFirstName = firstName,
                CreditCardLastName = lastName,
                CreditCardNumber = cardNumber
            };
            
            _ctx.Add(creditCard);
            await _ctx.SaveChangesAsync();

            return creditCard;
        }
        #endregion

        #region IdentityRole

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        }

        #endregion
        
    }
}