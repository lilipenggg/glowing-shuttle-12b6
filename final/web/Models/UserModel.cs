namespace web.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserCreditCardId { get; set; }
        public int? UserAwardPoints { get; set; }
        public string UserShippingAddress { get; set; }
        public string UserShippingAddress2 { get; set; }
        public string UserShippingCity { get; set; }
        public string UserShippingState { get; set; }
        public int UserShippingZipCode { get; set; }
        public string UserBillingAddress { get; set; }
        public string UserBillingAddress2 { get; set; }
        public string UserBillingCity { get; set; }
        public string UserBillingState { get; set; }
        public int UserBillingZipCode { get; set; }

        public CreditCardModel UserCreditCard { get; set; }
    }
}