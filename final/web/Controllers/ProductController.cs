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
    public class ProductController
    {
        private readonly IKioskRepository _repository;

        public ProductController(IKioskRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductModel>> List(string category)
        {
            var productModels = new List<ProductModel>();
            List<web.Data.Entities.Product> products;

            if (string.IsNullOrEmpty(category))
            {
                products = await _repository.GetProducts();
            }
            else
            {
                products = await _repository.GetProductByCategory(category);
            }

            productModels.AddRange(products.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductDescription = p.ProductDescription,
                ProductExpirationDate = p.ProductExpirationDate,
                ProductImage = p.ProductImage,
                ProductName = p.ProductName,
                ProductQuantity = p.ProductQuantity,
                ProductUnitPrice = p.ProductUnitPrice
            }));

            return productModels;
        }

    }
}