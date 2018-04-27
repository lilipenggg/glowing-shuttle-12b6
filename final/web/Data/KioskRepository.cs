using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Language;
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

        #region Product
        
        public async Task<List<Product>> GetProducts()
        {
            return await _ctx.Product.Include(p => p.ProductCategory).OrderBy(p => p.ProductName).ToListAsync();
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
            return await (from p in _ctx.Product
                    where p.ProductVendorId == vendorId
                    select p)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductByVendorName(string vendorName)
        {
            var applicationUser = await GetApplicationUserByUserName(vendorName);
            return await _ctx.Product.Where(p => p.ProductVendorId == applicationUser.Id).Include(p => p.ProductCategory).ToListAsync();
        }

        public async Task CreateProduct(ProductModel productModel, string userName)
        {
            var applicationUser = await GetApplicationUserByUserName(userName);
            
            _ctx.Add(new Product
            {
                ProductCategoryId = productModel.ProductCategoryId,
                ProductDescription = productModel.ProductDescription,
                ProductExpirationDate = productModel.ProductExpirationDate,
                ProductImage = productModel.ProductImage,
                ProductName = productModel.ProductName,
                ProductQuantity = productModel.ProductQuantity,
                ProductUnitPrice = productModel.ProductUnitPrice,
                ProductVendorId = applicationUser.Id
            });
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateProduct(ProductModel productModel, string userName)
        {   
            var applicationUser = await GetApplicationUserByUserName(userName);
            _ctx.Update(new Product
            {
                ProductCategoryId = productModel.ProductCategoryId,
                ProductDescription = productModel.ProductDescription,
                ProductExpirationDate = productModel.ProductExpirationDate,
                ProductImage = productModel.ProductImage,
                ProductName = productModel.ProductName,
                ProductQuantity = productModel.ProductQuantity,
                ProductUnitPrice = productModel.ProductUnitPrice,
                ProductVendorId = applicationUser.Id,
                ProductId = productModel.ProductId
            });

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteProduct(string productId)
        {
            var product = await GetProductById(productId);
            _ctx.Product.Remove(product);
            await _ctx.SaveChangesAsync();
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

        public async Task<List<OrderItem>> GetOrderItems()
        {
            return await _ctx.OrderItem.OrderBy(oi => oi.OrderItemId).ToListAsync();
        }

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

        public async Task CreateOrder(OrderModel orderModel, List<ShoppingCartItem> shoppingCartItems, string userName)
        {   
            // create credit card entry
            var creditCard = await CreateCreditCard(orderModel.OrderCreditCard.CreditCardCvv,
                orderModel.OrderCreditCard.CreditCardExpirationDate,
                orderModel.OrderCreditCard.CreditCardFirstName, orderModel.OrderCreditCard.CreditCardLastName,
                orderModel.OrderCreditCard.CreditCardNumber);
            
            // create order
            var order = new Order
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
                OrderShippingZipCode = orderModel.OrderShippingZipCode
            };
            
            var orderTotal = await _shoppingCart.GetShoppingCartTotal();

            if (userName != null)
            {
                var user = await GetApplicationUserByUserName(userName);
                var points = user.ApplicationUserAwardPoints;
                
                // Update current user's award points - using order total before tax
                var orderTotalBeforeTax = await _shoppingCart.GetShoppingCartTotalBeforeTax();
                
                // Update the order total based on the award points that is being applied
                if (orderModel.OrderAppliedAwardPoints != 0)
                {
                    var credit = orderModel.OrderAppliedAwardPoints / 30 * 10;
                    points = points - orderModel.OrderAppliedAwardPoints + orderModel.OrderAppliedAwardPoints % 30;

                    // Apply the credit from award points to the order total before tax
                    orderTotalBeforeTax -= credit;
                    orderTotal *= 1 + TaxPercentage.Tax.Value;
                    orderModel.OrderTotal = orderTotal;
                }
                
                // Add in the collected award points based on the latest order total
                points += (int) Math.Floor(orderTotalBeforeTax) * 3;
                await UpdateApplicationUserAwardPoints(points, userName);
                
                // Associate customer with this order
                order.OrderCustomerId = user.Id;
            }

            order.OrderTotal = orderTotal;

            _ctx.Order.Add(order);
            await _ctx.SaveChangesAsync();
            
            // create assoicated order items
            await CreateOrderItems(shoppingCartItems, order);
        }

        #endregion

        #region ApplicationUser

        public async Task<List<ApplicationUser>> GetApplicationUsers()
        {
            return await _userManager.Users.OrderBy(u => u.Id).ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetApplicationUserByRole(string role)
        {
            var result = await _userManager.GetUsersInRoleAsync(role);
            return new List<ApplicationUser>(result);
        }

        public async Task<ApplicationUser> GetApplicationUserByEmail(string email)
        {
            return await _ctx.ApplicationUser.SingleOrDefaultAsync(a => a.ApplicationUserEmail == email);
        }

        public async Task<ApplicationUser> GetApplicationUserByUserName(string userName)
        {
            return await _ctx.ApplicationUser.SingleOrDefaultAsync(a => a.UserName == userName);
        }
        
        public async Task UpdateApplicationUserAwardPoints(int? awardPoints, string userName)
        {
            var user = await GetApplicationUserByUserName(userName);
            user.ApplicationUserAwardPoints = awardPoints;
            await _ctx.SaveChangesAsync();
        }
        
        /// <summary>
        /// Get a list of customers that have bought a specific vendor's product within the last month
        /// </summary>
        /// <param name="vendorUserName">Vendor's username</param>
        /// <param name="distinct">Return a list of distinct customers if true, otherwise not distinct</param>
        /// <returns></returns>
        public async Task<List<ApplicationUser>> GetApplicationUserPurchasedLastMonth(string vendorUserName)
        {
            // Retrieve the vendor user id
            var vendorUser = await GetApplicationUserByUserName(vendorUserName);

            var products = await GetProducts();
            var orderItems = await GetOrderItems();
            var orders = await GetOrders();
            var users = await GetApplicationUserByRole(RoleType.Customer.Value);

            var startDayOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var firstDayLastMonth = startDayOfThisMonth.AddMonths(-1);
            var lastDayLastMonth = startDayOfThisMonth.AddDays(-1);
            
            // Perform inner join to find out customers that have bought a vendor's product within the last month
            var customers = (from p in products
                        join oi in orderItems on p.ProductId equals oi.OrderItemProductId
                        join o in orders on oi.OrderItemOrderId equals o.OrderId
                        join u in users on o.OrderCustomerId equals u.Id
                        where p.ProductVendorId == vendorUser.Id &&
                              o.OrderDateTime >= firstDayLastMonth &&
                              o.OrderDateTime <= lastDayLastMonth
                        select u)
                    .Distinct()
                    .ToList();

            return customers;
        }
        
        /// <summary>
        /// Get a list of customers who have purchased a specific vendor's product for a number of times within last month
        /// </summary>
        /// <param name="vendorUserName">the vendor name</param>
        /// <param name="count">the number of times that the customers have made an purchase last month</param>
        /// <returns></returns>
        public async Task<List<ApplicationUser>> GetApplicationUserPurchasedNumOfTimes(string vendorUserName, int count)
        {
            // Retrieve the vendor user id
            var vendorUser = await GetApplicationUserByUserName(vendorUserName);

            var products = await GetProducts();
            var orderItems = await GetOrderItems();
            var orders = await GetOrders();
            var users = await GetApplicationUserByRole(RoleType.Customer.Value);

            var startDayOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var firstDayLastMonth = startDayOfThisMonth.AddMonths(-1);
            var lastDayLastMonth = startDayOfThisMonth.AddDays(-1);
            
            // Perform inner join to find out customers that have bought a vendor's product within the last month
            var customerOrders = (from p in products
                        join oi in orderItems on p.ProductId equals oi.OrderItemProductId
                        join o in orders on oi.OrderItemOrderId equals o.OrderId
                        join u in users on o.OrderCustomerId equals u.Id
                        where p.ProductVendorId == vendorUser.Id &&
                              o.OrderDateTime >= firstDayLastMonth &&
                              o.OrderDateTime <= lastDayLastMonth
                        select new {u.Id, oi.OrderItemId})
                    .ToList();
            
            
            var customerCount = customerOrders.GroupBy(u => u.Id)
                .Select(u => new
                {
                    UserId = u.Key,
                    Count = u.Count()
                })
                .Where(u => u.Count == count);

            var applicationUsers = new List<ApplicationUser>();
            foreach (var c in customerCount)
            {
                applicationUsers.Add(await _userManager.FindByIdAsync(c.UserId));
            }

            return applicationUsers;
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