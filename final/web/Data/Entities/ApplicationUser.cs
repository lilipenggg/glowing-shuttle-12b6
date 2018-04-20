﻿using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            Order = new HashSet<Order>();
            Product = new HashSet<Product>();
        }

        public string ApplicationUserId { get; set; }
        public string ApplicationUserFirstName { get; set; }
        public string ApplicationUserLastName { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserPassword { get; set; }
        public string ApplicationUserTypeId { get; set; }
        public string ApplicationUserPhoneNumber { get; set; }
        public int? ApplicationUserAwardPoints { get; set; }
        public string ApplicationUserCreditCardId { get; set; }

        public CreditCard ApplicationUserCreditCard { get; set; }
        public UserType ApplicationUserType { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
