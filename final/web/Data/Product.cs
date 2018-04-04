using System;
using System.Collections.Generic;

namespace web.Data
{
    public partial class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string SellerId { get; set; }

        public User Seller { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
