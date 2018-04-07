using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class User
    {
        public User()
        {
            OrderOrderBuyer = new HashSet<Order>();
            OrderOrderSeller = new HashSet<Order>();
            Product = new HashSet<Product>();
        }

        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        public string UserCreditCard { get; set; }
        public int? UserAwardPoints { get; set; }

        public ICollection<Order> OrderOrderBuyer { get; set; }
        public ICollection<Order> OrderOrderSeller { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
