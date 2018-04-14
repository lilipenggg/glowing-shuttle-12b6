using System;
using System.Collections.Generic;

namespace web.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int OrderAppliedAwardPoints { get; set; }
        public decimal OrderAppliedDiscount { get; set; }
        public decimal OrderTotalAmount { get; set; }

        public UserModel OrderBuyer { get; set; }
        public UserModel OrderSeller { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}