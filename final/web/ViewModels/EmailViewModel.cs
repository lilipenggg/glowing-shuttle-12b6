using System.Collections.Generic;
using web.Models;

namespace web.ViewModels
{
    public class EmailViewModel
    {
        public EmailModel Email { get; set; }
        public List<ApplicationUserModel> Users { get; set; }
        public string Count { get; set; }
    }
}