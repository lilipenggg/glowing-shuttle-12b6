using System;
using System.Collections.Generic;

namespace web.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            OrderItem = new HashSet<OrderItemModel>();
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

        public CategoryModel ProductCategory { get; set; }
        public ApplicationUserModel ProductVendor { get; set; }
        public ICollection<OrderItemModel> OrderItem { get; set; }
    }
}