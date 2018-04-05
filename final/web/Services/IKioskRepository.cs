using System.Collections.Generic;
using web.Data;
using web.Data.Entities;

namespace web.Services
{
    public interface IKioskRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Order> GetAllOrders();
    }
}