using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class BusinessUser
    {
        public BusinessUser()
        {
            OrderOrderBuyer = new HashSet<Order>();
            OrderOrderSeller = new HashSet<Order>();
        }

        public string BusinessUserId { get; set; }
        public string BusinessUserCreditCardId { get; set; }
        public int? BusinessUserAwardPoints { get; set; }
        public string BusinessUserShippingAddress { get; set; }
        public string BusinessUserShippingAddress2 { get; set; }
        public string BusinessUserShippingCity { get; set; }
        public string BusinessUserShippingState { get; set; }
        public int BusinessUserShippingZipCode { get; set; }
        public string BusinessUserBillingAddress { get; set; }
        public string BusinessUserBillingAddress2 { get; set; }
        public string BusinessUserBillingCity { get; set; }
        public string BusinessUserBillingState { get; set; }
        public int BusinessUserBillingZipCode { get; set; }

        public CreditCard BusinessUserCreditCard { get; set; }
        public ApplicationUser BusinessUserNavigation { get; set; }
        public ICollection<Order> OrderOrderBuyer { get; set; }
        public ICollection<Order> OrderOrderSeller { get; set; }
    }
}
