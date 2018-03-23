using System;

namespace web.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Image { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SellerId { get; set; }
    }
}