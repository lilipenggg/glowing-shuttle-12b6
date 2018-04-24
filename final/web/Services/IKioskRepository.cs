using System;
using System.Collections.Generic;
using System.Security.Claims;
using web.Data;
using web.Data.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using web.Enums;

namespace web.Services
{
    public interface IKioskRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(string productId);
        Task<List<Product>> GetProductByCategory(string category);
        Task<List<Product>> GetProductByVendorId(string vendorId);
        Task<List<Product>> GetProductByVendorName(string vendorName);   
        Task CreateProduct(Models.ProductModel productModel, string userName);
        Task UpdateProduct(Models.ProductModel productModel, string userName);
        Task DeleteProduct(string productId);

        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(string categoryId);
        Task<Category> GetCategoryByName(string categoryName);

        Task<List<ShoppingCartItem>> GetShoppingCartItems(string cartId);

        Task CreateOrderItems(List<ShoppingCartItem> shoppingCartItems, Order order);
        
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(string id);
        Task CreateOrder(web.Models.OrderModel orderModel, List<ShoppingCartItem> shoppingCartItems);

        Task<CreditCard> CreateCreditCard(int cvv, DateTime expirationDate, string firstName, string lastName,
            string cardNumber);

        Task<List<IdentityRole>> GetAllRoles();

        Task<ApplicationUser> GetApplicationUserByUserName(string userName);

    }
}