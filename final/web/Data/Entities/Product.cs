using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public DateTime? ProductExpirationDate { get; set; }
        public double ProductUnitPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductSellerId { get; set; }
        public string ProductCategoryId { get; set; }

        public Category ProductCategory { get; set; }
        public BusinessUser ProductSeller { get; set; }
        public OrderItem OrderItem { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
