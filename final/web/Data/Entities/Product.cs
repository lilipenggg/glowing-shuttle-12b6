using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public DateTime? ProductExpirationDate { get; set; }
        public double ProductUnitPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductVendorId { get; set; }
        public string ProductCategoryId { get; set; }

        public Category ProductCategory { get; set; }
        public ApplicationUser ProductVendor { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
