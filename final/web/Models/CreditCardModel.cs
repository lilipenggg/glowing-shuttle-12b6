using System;
using System.Collections.Generic;

namespace web.Models
{
    public class CreditCardModel
    {
        public CreditCardModel()
        {
            BusinessUser = new HashSet<BusinessUserModel>();
        }

        public string CreditCardId { get; set; }
        public string CreditCardFirstName { get; set; }
        public string CreditCardLastName { get; set; }
        public DateTime CreditCardExpirationDate { get; set; }
        public string CreditCardNumber { get; set; }
        public int CreditCardCvv { get; set; }
        public string CreditCardUserId { get; set; }

        public GuestUserModel GuestUser { get; set; }
        public ICollection<BusinessUserModel> BusinessUser { get; set; }
    }
}