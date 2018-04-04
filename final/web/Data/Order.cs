using System;
using System.Collections.Generic;

namespace web.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public string Id { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int AppliedAwardPoints { get; set; }
        public decimal AppliedDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }

        public User Buyer { get; set; }
        public User Seller { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
