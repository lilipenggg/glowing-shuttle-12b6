using System;
using System.Collections;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
    }
}