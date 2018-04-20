using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public string OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int? OrderAppliedAwardPoints { get; set; }
        public double? OrderAppliedDiscount { get; set; }
        public double? OrderTotal { get; set; }
        public string OrderCustomerId { get; set; }
        public string OrderShippingAddress1 { get; set; }
        public string OrderShippingAddress2 { get; set; }
        public string OrderShippingCity { get; set; }
        public string OrderShippingState { get; set; }
        public int OrderShippingZipCode { get; set; }
        public string OrderBillingAddress1 { get; set; }
        public string OrderBillingAddress2 { get; set; }
        public string OrderBillingCity { get; set; }
        public string OrderBillingState { get; set; }
        public string OrderBillingZipCode { get; set; }
        public string OrderShippingFirstName { get; set; }
        public string OrderShippingLastName { get; set; }
        public string OrderBillingFirstName { get; set; }
        public string OrderBillingLastName { get; set; }
        public string OrderCreditCardId { get; set; }

        public CreditCard OrderCreditCard { get; set; }
        public ApplicationUser OrderCustomer { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
