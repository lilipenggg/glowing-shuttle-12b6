using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class ShoppingCartItem
    {
        public string ShoppingCartItemId { get; set; }
        public string ProductId { get; set; }
        public int ShoppingCartItemAmount { get; set; }
        public string ShoppingCartId { get; set; }

        public Product Product { get; set; }
    }
}
