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
        public string OrderSellerId { get; set; }
        public string OrderBuyerId { get; set; }
        public string OrderGuestBuyerId { get; set; }

        public BusinessUser OrderBuyer { get; set; }
        public GuestUser OrderGuestBuyer { get; set; }
        public BusinessUser OrderSeller { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
