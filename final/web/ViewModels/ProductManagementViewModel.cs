using web.Models;
using System.Collections.Generic;

namespace web.ViewModels
{
    public class ProductManagementViewModel
    {
        public ProductModel Product { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}