using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class OrderItem
    {
        public string OrderItemId { get; set; }
        public int? OrderItemQuantity { get; set; }
        public string OrderItemOrderId { get; set; }

        public Product OrderItemNavigation { get; set; }
        public Order OrderItemOrder { get; set; }
    }
}
