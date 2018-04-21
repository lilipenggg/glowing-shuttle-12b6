using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace web.Data.Entities
{
    public partial class UserType
    {
        public UserType()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }

        public string UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
