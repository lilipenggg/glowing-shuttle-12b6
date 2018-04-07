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
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IKioskRepository _repository;

        public HomeController(IMailService mailService, IKioskRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }
        
        [Route("home/index")]
        public async Task<IActionResult> Index()
        {
            var productModels = new List<ProductModel>();
            var products = await _repository.GetProducts();

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
            
            return View(productModels);
        }

        [Route("home/about")]
        public async Task<IActionResult> About()
        {
            return View();
        }

        [HttpGet("contact")]
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        
        [HttpPost("contact")]
        public async Task<IActionResult> Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                // Send the email
                _mailService.SendMail("lilypenggg@gmail.com", contactViewModel.Subject,
                    $"From: {contactViewModel.Name} {contactViewModel.Email}, Message: {contactViewModel.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                // Show the error
            }
            
            return View();
        }

    }
    
}
