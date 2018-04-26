using web.Models;

namespace web.ViewModels
{
    public class OrderCheckoutViewModel
    {
        public OrderModel Order { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }
    }
}