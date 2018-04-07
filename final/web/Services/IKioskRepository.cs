using System.Collections.Generic;
using web.Data;
using web.Data.Entities;
using System.Threading.Tasks;

namespace web.Services
{
    public interface IKioskRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(string id);

        Task<List<ShoppingCartItem>> GetShoppingCartItems(string cartId);
        
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(string id);

        Task<List<User>> GetUsers();
        Task<User> GetUserById(string id);
    }
}