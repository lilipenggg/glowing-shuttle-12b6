using System;
using System.Collections.Generic;

namespace web.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            OrderItem = new HashSet<OrderItemModel>();
        }

        public string OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int? OrderAppliedAwardPoints { get; set; }
        public double? OrderAppliedDiscount { get; set; }
        public double? OrderTotal { get; set; }
        public string OrderBuyerId { get; set; }
        public string OrderGuestBuyerId { get; set; }

        public BusinessUserModel OrderBuyer { get; set; }
        public GuestUserModel OrderGuestBuyer { get; set; }
        public ICollection<OrderItemModel> OrderItem { get; set; }
    }
}