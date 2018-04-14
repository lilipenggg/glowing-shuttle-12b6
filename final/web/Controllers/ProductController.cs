using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;
using web.ViewModels;

namespace web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IKioskRepository _repository;

        public ProductController(IKioskRepository repository)
        {
            _repository = repository;
        }

        public async Task<ViewResult> List(string category)
        {
            var model = new ProductListViewModel();
            //var productModels = new List<ProductModel>();
            List<web.Data.Entities.Product> products;

            model.ProductModels = new List<ProductModel>();

            if (string.IsNullOrEmpty(category))
            {
                products = await _repository.GetProducts();
                model.CurrentCategory = "All products";
            }
            else
            {
                products = await _repository.GetProductByCategory(category);
                model.CurrentCategory = category;
            }

            model.ProductModels.AddRange(products.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductDescription = p.ProductDescription,
                ProductExpirationDate = p.ProductExpirationDate,
                ProductImage = p.ProductImage,
                ProductName = p.ProductName,
                ProductQuantity = p.ProductQuantity,
                ProductUnitPrice = p.ProductUnitPrice
            }));

            return View(model);
        }

        public async Task<IActionResult> Details(string productId)
        {
            var product = await _repository.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }
            
            var productModel = new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductCategory = new CategoryModel
                {
                    CategoryId = product.ProductCategory.CategoryId,
                    CategoryName = product.ProductCategory.CategoryName
                },
                ProductExpirationDate = product.ProductExpirationDate,
                ProductImage = product.ProductImage,
                ProductQuantity = product.ProductQuantity,
                ProductUnitPrice = product.ProductUnitPrice
            };
            return View(productModel);
        }

    }
}