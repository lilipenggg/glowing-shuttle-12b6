using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace web.Data.Entities
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Order = new HashSet<Order>();
            Product = new HashSet<Product>();
        }

        public string ApplicationUserFirstName { get; set; }
        public string ApplicationUserLastName { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserPhoneNumber { get; set; }
        public int? ApplicationUserAwardPoints { get; set; }
        public string ApplicationUserCreditCardId { get; set; }

        public CreditCard ApplicationUserCreditCard { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
