using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class CreditCard
    {
        public CreditCard()
        {
            BusinessUser = new HashSet<BusinessUser>();
        }

        public string CreditCardId { get; set; }
        public string CreditCardFirstName { get; set; }
        public string CreditCartLastName { get; set; }
        public DateTime CreditCardExpirationDate { get; set; }
        public string CreditCardNumber { get; set; }
        public int CreditCardCvv { get; set; }
        public string CreditCardUserId { get; set; }

        public GuestUser GuestUser { get; set; }
        public ICollection<BusinessUser> BusinessUser { get; set; }
    }
}
