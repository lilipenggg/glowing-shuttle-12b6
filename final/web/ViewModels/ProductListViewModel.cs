using System.Collections.Generic;
using web.Models;

namespace web.ViewModels
{
    public class ProductListViewModel
    {
        public List<ProductModel> ProductModels { get; set; }
        public string CurrentCategory { get; set; }
    }
}