using System.Collections.Generic;
using System.Linq;

namespace web.Data
{
    public class KioskRepository : IKioskRepository
    {
        private readonly kioskContext _ctx;

        public KioskRepository(kioskContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Product
                .OrderBy(p => p.Name)
                .ToList();
        }
        
    }
}