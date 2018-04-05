using System.Collections.Generic;

namespace web.Data
{
    public interface IKioskRepository
    {
        IEnumerable<Product> GetAllProducts();
    }
}