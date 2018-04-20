using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class CreditCard
    {
        public CreditCard()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
            Order = new HashSet<Order>();
        }

        public string CreditCardId { get; set; }
        public string CreditCardFirstName { get; set; }
        public string CreditCardLastName { get; set; }
        public DateTime? CreditCardExpirationDate { get; set; }
        public string CreditCardNumber { get; set; }
        public int? CreditCardCvv { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
