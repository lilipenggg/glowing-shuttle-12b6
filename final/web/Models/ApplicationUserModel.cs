using System.Collections.Generic;

namespace web.Models
{
    public class ApplicationUserModel
    {
        public ApplicationUserModel()
        {
            Order = new HashSet<OrderModel>();
            Product = new HashSet<ProductModel>();
        }

        public string ApplicationUserId { get; set; }
        public string ApplicationUserFirstName { get; set; }
        public string ApplicationUserLastName { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserPassword { get; set; }
        public string ApplicationUserTypeId { get; set; }
        public string ApplicationUserPhoneNumber { get; set; }
        public int? ApplicationUserAwardPoints { get; set; }
        public string ApplicationUserCreditCardId { get; set; }

        public CreditCardModel ApplicationUserCreditCard { get; set; }
        public UserTypeModel ApplicationUserType { get; set; }
        public ICollection<OrderModel> Order { get; set; }
        public ICollection<ProductModel> Product { get; set; }
    }
}