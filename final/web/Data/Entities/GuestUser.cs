using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class GuestUser
    {
        public GuestUser()
        {
            Order = new HashSet<Order>();
        }

        public string GuestUserId { get; set; }
        public string GuestUserFirstName { get; set; }
        public string GuestUserLastName { get; set; }
        public string GuestUserShippingAddress { get; set; }
        public string GuestUserShippingAddress2 { get; set; }
        public string GuestUserShippingCity { get; set; }
        public string GuestUserShippingState { get; set; }
        public int GuestUserShippingZipCode { get; set; }
        public string GuestUserBillingAddress { get; set; }
        public string GuestUserBillingAddress2 { get; set; }
        public string GuestUserBillingCity { get; set; }
        public string GuestUserBillingZipState { get; set; }
        public int GuestUserBillingZipCode { get; set; }

        public CreditCard GuestUserNavigation { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
