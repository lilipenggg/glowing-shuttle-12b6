using System;
using System.Collections.Generic;

namespace web.Data.Entities
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            Product = new HashSet<Product>();
        }

        public string ApplicationUserId { get; set; }
        public string ApplicationUserFirstName { get; set; }
        public string ApplicationUserLastName { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserPassword { get; set; }
        public string ApplicationUserTypeId { get; set; }

        public UserType ApplicationUserType { get; set; }
        public BusinessUser BusinessUser { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
