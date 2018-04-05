using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class OrderItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string OrderId { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
