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

        /// <summary>
        /// Return a partial view of the details of the product for vendor to make modification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _repository.GetCategories();

            var productModel = new ProductModel
            {
                ProductId = product.ProductId,
                ProductCategory = new CategoryModel
                {
                    CategoryId = product.ProductCategory.CategoryId,
                    CategoryName = product.ProductCategory.CategoryName
                },
                ProductCategoryId = product.ProductCategoryId,
                ProductDescription = product.ProductDescription,
                ProductExpirationDate = product.ProductExpirationDate,
                ProductImage = product.ProductImage,
                ProductName = product.ProductName,
                ProductQuantity = product.ProductQuantity,
                ProductUnitPrice = product.ProductUnitPrice,
                ProductVendorId = product.ProductVendorId
            };

            var model = new ProductManagementViewModel
            {
                Product = productModel,
                Categories = categories.Select(c => new CategoryModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }).OrderBy(c => c.CategoryName).ToList()
            };

            return PartialView("Edit", model);
        }

        /// <summary>
        /// Collect the data that vendor has filled out in the Edit form and save them to database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productManagementViewModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProductManagementViewModel productManagementViewModel)
        {
            var product = productManagementViewModel.Product;
            if (id != product.ProductId)
            {
                return NotFound();
            }

            var applicationUser = await _repository.GetApplicationUserByUserName(User.Identity.Name);
            var existingProduct = await _repository.GetProductById(id);

            // Not allow vendor who did not create the product to make the modification 
            if (applicationUser.Id != existingProduct.ProductVendorId)
            {
                return NotFound("You're the authorized user to modify this item...");
            }
            
            if (ModelState.IsValid)
            {
                await _repository.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }

            return View(productManagementViewModel);
        }

        /// <summary>
        /// Return a partial view contains the information about this product with a Delete button
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            
            var productModel = new ProductModel
            {
                ProductId = product.ProductId,
                ProductCategory = new CategoryModel
                {
                    CategoryId = product.ProductCategory.CategoryId,
                    CategoryName = product.ProductCategory.CategoryName
                },
                ProductCategoryId = product.ProductCategoryId,
                ProductDescription = product.ProductDescription,
                ProductExpirationDate = product.ProductExpirationDate,
                ProductImage = product.ProductImage,
                ProductName = product.ProductName,
                ProductQuantity = product.ProductQuantity,
                ProductUnitPrice = product.ProductUnitPrice,
                ProductVendorId = product.ProductVendorId
            };

            return View(productModel);
        }

        /// <summary>
        /// Query the database to perform the delete product action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _repository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
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