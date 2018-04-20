using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class OrderItem
    {
        public string OrderItemId { get; set; }
        public int OrderItemQuantity { get; set; }
        public string OrderItemOrderId { get; set; }
        public string OrderItemProductId { get; set; }

        public Order OrderItemOrder { get; set; }
        public Product OrderItemProduct { get; set; }
    }
}
