using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Product
                .OrderBy(p => p.Name)
                .ToList();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Order
                .OrderBy(o => o.OrderDateTime)
                .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}