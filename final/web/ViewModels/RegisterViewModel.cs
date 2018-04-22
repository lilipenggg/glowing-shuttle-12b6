using web.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace web.ViewModels
{
    public class RegisterViewModel
    {
        public List<UserRoleModel> UserRoleModels { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }
    }
}