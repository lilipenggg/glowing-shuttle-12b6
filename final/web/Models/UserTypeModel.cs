using System.Collections.Generic;

namespace web.Models
{
    public class UserTypeModel
    {
        public UserTypeModel()
        {
            ApplicationUser = new HashSet<ApplicationUserModel>();
        }

        public string UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public ICollection<ApplicationUserModel> ApplicationUser { get; set; }
    }
}