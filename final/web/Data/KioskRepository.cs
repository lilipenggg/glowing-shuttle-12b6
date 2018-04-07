using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using web.Services;
using web.Data.Entities;

namespace web.Data
{
    public class KioskRepository : IKioskRepository
    {
        private readonly KioskContext _ctx;

        public KioskRepository(KioskContext ctx)
        {
            _ctx = ctx;
        }

        #region Product
        
        public async Task<List<Product>> GetProducts()
        {
            return await _ctx.Product.OrderBy(p => p.ProductName).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _ctx.Product.SingleOrDefaultAsync(p => p.ProductId == id);
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

        #region Order
        
        public async Task<List<Order>> GetOrders()
        {
            return await (from o in _ctx.Order
                orderby o.OrderId
                select o).ToListAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _ctx.Order.SingleOrDefaultAsync(o => o.OrderId == id);
        }

        #endregion
        
        #region User

        public async Task<List<User>> GetUsers()
        {
            return await (from u in _ctx.User
                orderby u.UserId
                select u).ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _ctx.User.SingleOrDefaultAsync(u => u.UserId == id);
        }
        
        #endregion
        
    }
}