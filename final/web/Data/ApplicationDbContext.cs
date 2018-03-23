using Microsoft.EntityFrameworkCore;
using web.Data.Entities;

namespace web.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Only create DbSet for things that you query directly against
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerUser> CustomerUsers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}