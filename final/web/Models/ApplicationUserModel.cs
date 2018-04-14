using System.Collections.Generic;

namespace web.Models
{
    public class ApplicationUserModel
    {
        public string ApplicationUserId { get; set; }
        public string ApplicationUserFirstName { get; set; }
        public string ApplicationUserLastName { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserPassword { get; set; }
        public string ApplicationUserTypeId { get; set; }

        public UserTypeModel ApplicationUserType { get; set; }
        public BusinessUserModel BusinessUser { get; set; }
    }
}