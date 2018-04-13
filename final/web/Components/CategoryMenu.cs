using Microsoft.AspNetCore.Mvc;
using web.Services;
using System.Threading.Tasks;
using web.Models;
using System.Collections.Generic;
using System.Linq;

namespace web.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly IKioskRepository _repository;

        public CategoryMenu(IKioskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _repository.GetCategories();
            var categoryModels = new List<CategoryModel>();
            
            categoryModels.AddRange(categories.Select(c => new CategoryModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList());

            return View(categoryModels);
        }
    }
}