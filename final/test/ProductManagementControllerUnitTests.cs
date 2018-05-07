using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Controllers;
using web.Data;
using web.Data.Entities;
using web.Services;
using Xunit;

namespace test
{
    public class ProductManagementControllerUnitTests
    {
        private readonly IKioskRepository _repository;

        public ProductManagementControllerUnitTests(IKioskRepository repository)
        {
            _repository = repository;
        }
        
        [Fact]
        public async Task Index_ReturnsAViewResult_ContainsAllProducts()
        {
            // arrange 
            var productController = new ProductController(_repository);
            
            // act
            var result = productController.Index();
            
            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var products = Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);
            Assert.Equal((await _repository.GetProducts()).Count, products.Count);
        }
        
    }
}
