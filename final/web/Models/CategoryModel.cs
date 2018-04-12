using System;
using System.Collections.Generic;

namespace web.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            Product = new HashSet<ProductModel>();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProductModel> Product { get; set; }
    }
}