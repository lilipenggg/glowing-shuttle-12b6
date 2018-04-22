using web.Models;
using System.Collections.Generic;

namespace web.ViewModels
{
    public class RegisterViewModel
    {
        public List<UserTypeModel> UserTypes { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }
    }
}