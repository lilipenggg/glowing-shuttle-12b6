using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class User
    {
        public User()
        {
            OrderBuyer = new HashSet<Order>();
            OrderSeller = new HashSet<Order>();
            Product = new HashSet<Product>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string CreditCard { get; set; }
        public int? AwardPoints { get; set; }

        public ICollection<Order> OrderBuyer { get; set; }
        public ICollection<Order> OrderSeller { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
