using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Controllers;
using web.Data.Entities;
using web.Models;
using web.Services;
using Xunit;

namespace test
{
    public class OrderManagementControllerUnitTests
    {
        private readonly IKioskRepository _repository;
        private readonly ShoppingCartModel _shoppingCart;

        public OrderManagementControllerUnitTests(IKioskRepository repository, ShoppingCartModel shoppingCart)
        {
            _repository = repository;
            _shoppingCart = shoppingCart;
        }
        
        [Fact]
        public async Task List_ReturnsAViewResult_ContainsAllOrders()
        {
            // arrange 
            var orderController = new OrderController(_repository, _shoppingCart);
            
            // act
            var result = orderController.List();
            
            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var orders = Assert.IsAssignableFrom<List<Order>>(viewResult.ViewData.Model);
            Assert.Equal((await _repository.GetOrders()).Count, orders.Count);
        }
    }
}