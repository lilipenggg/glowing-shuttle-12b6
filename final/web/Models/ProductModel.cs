using System;

namespace web.Models
{
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public DateTime? ProductExpirationDate { get; set; }
        public double ProductUnitPrice { get; set; }
        public int ProductQuantity { get; set; }
        
        public BusinessUserModel Seller { get; set; }
        public OrderItemModel OrderItem { get; set; }
        public CategoryModel ProductCategory { get; set; }
    }
}