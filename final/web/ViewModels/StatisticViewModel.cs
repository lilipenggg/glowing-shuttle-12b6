using System.Collections.Generic;
using web.Models;

namespace web.ViewModels
{
    public class StatisticViewModel
    {
        public List<ApplicationUserModel> Users { get; set; }
        public string NumOfPurchases { get; set; }
    }
}