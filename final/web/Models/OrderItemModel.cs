namespace web.Models
{
    public class OrderItemModel
    {
        public string OrderItemId { get; set; }
        public int OrderItemQuantity { get; set; }

        public ProductModel Product { get; set; }
        public OrderModel Order { get; set; }
    }
}