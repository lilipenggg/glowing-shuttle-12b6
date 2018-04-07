using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using web.Data;
using web.Data.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace web.Models
{
    public class ShoppingCartModel
    {
        private readonly KioskContext _context;
        
        private ShoppingCartModel(KioskContext context)
        {
            _context = context;
        }
        
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCartModel GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext.Session;

            var context = services.GetService<KioskContext>();
            
            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            
            session.SetString("CartId", cartId);
            
            return new ShoppingCartModel(context) { ShoppingCartId = cartId };
        }

        public async Task AddToCart(Product product, int amount)
        {
            var shoppingCartItem =
                await _context.ShoppingCartItem.SingleOrDefaultAsync(
                    s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    ShoppingCartItemAmount = 1
                };

                _context.ShoppingCartItem.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.ShoppingCartItemAmount++;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCart(Product product)
        {
            var shoppingCartItem = await _context.ShoppingCartItem.SingleOrDefaultAsync(
                s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.ShoppingCartItemAmount > 1)
                {
                    shoppingCartItem.ShoppingCartItemAmount--;
                    localAmount = shoppingCartItem.ShoppingCartItemAmount;
                }
                else
                {
                    _context.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }
            
            await _context.SaveChangesAsync();

            return localAmount;
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems = await _context.ShoppingCartItem
                       .Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.Product)
                       .ToListAsync());
        }

        public async Task CleanCart()
        {
            var cartItems =  await _context
                .ShoppingCartItem
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .ToListAsync();
            
            _context.ShoppingCartItem.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<double> GetShoppingCartTotal()
        {
            var shoppingCartItems = await _context.ShoppingCartItem
                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                    .ToListAsync();
            
            var total = shoppingCartItems
                .Select(c => c.Product.ProductUnitPrice * c.ShoppingCartItemAmount)
                .Sum();

            return total;
        }
    }
}