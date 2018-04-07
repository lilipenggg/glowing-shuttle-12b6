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

        public User OrderBuyer { get; set; }
        public User OrderSeller { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
