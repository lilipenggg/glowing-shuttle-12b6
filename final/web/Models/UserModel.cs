using System.Collections.Generic;

namespace web.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        public string UserCreditCard { get; set; }
        public int UserAwardPoints { get; set; }

        public ICollection<OrderModel> OrderBuyer { get; set; }
        public ICollection<OrderModel> OrderSeller { get; set; }
        public ICollection<ProductModel> Product { get; set; }
    }
}