using System.Collections.Generic;

namespace web.Models
{
    public class BusinessUserModel
    {
        public BusinessUserModel()
        {
            OrderOrderBuyer = new HashSet<OrderModel>();
            OrderOrderSeller = new HashSet<OrderModel>();
            Product = new HashSet<ProductModel>();
        }

        public string BusinessUserId { get; set; }
        public string BusinessUserCreditCardId { get; set; }
        public int? BusinessUserAwardPoints { get; set; }
        public string BusinessUserShippingAddress { get; set; }
        public string BusinessUserShippingAddress2 { get; set; }
        public string BusinessUserShippingCity { get; set; }
        public string BusinessUserShippingState { get; set; }
        public int BusinessUserShippingZipCode { get; set; }
        public string BusinessUserBillingAddress { get; set; }
        public string BusinessUserBillingAddress2 { get; set; }
        public string BusinessUserBillingCity { get; set; }
        public string BusinessUserBillingState { get; set; }
        public int BusinessUserBillingZipCode { get; set; }

        public CreditCardModel BusinessUserCreditCard { get; set; }
        public ApplicationUserModel BusinessApplicationUser { get; set; }
        public ICollection<OrderModel> OrderOrderBuyer { get; set; }
        public ICollection<OrderModel> OrderOrderSeller { get; set; }
        public ICollection<ProductModel> Product { get; set; }
    }
}