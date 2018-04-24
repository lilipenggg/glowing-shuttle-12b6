using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Enums;
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

        /// <summary>
        /// Return a list of product with management tools when user with Vendor type login
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetProductByVendorName(User.Identity.Name);
            
            var productModels = products.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductCategory = new CategoryModel
                {
                    CategoryId = p.ProductCategory.CategoryId,
                    CategoryName = p.ProductCategory.CategoryName
                },
                ProductDescription = p.ProductDescription,
                ProductExpirationDate = p.ProductExpirationDate,
                ProductImage = p.ProductImage,
                ProductName = p.ProductName,
                ProductQuantity = p.ProductQuantity,
                ProductUnitPrice = p.ProductUnitPrice
            }).ToList();

            return View(productModels);
        }

        /// <summary>
        /// Retrieve a list of categories and return to the partial view of the product creation partial view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _repository.GetCategories();
            var model = new ProductManagementViewModel
            {
                Product = new ProductModel(),
                Categories = categories.Select(c => new CategoryModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }).OrderBy(c => c.CategoryName).ToList()
            };
            return PartialView("Create", model);
        }

        /// <summary>
        /// Create a product and associates it with the user who is currently logged in
        /// </summary>
        /// <param name="productManagementViewModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductManagementViewModel productManagementViewModel)
        {
            var product = productManagementViewModel.Product;

            if (ModelState.IsValid)
            {
                await _repository.CreateProduct(product, User.Identity.Name);

                return RedirectToAction(nameof(Index));
            }

            return View(productManagementViewModel);
        }

        public async Task<ViewResult> List(string category)
        {
            var model = new ProductListViewModel();
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