using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Data;
using web.Services;
using web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly kioskContext _context;

        public HomeController(IMailService mailService, kioskContext context)
        {
            _mailService = mailService;
            _context = context;
        }
        
        [Route("home/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("home/about")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }
        
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel contactViewModel)
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

        [Route("home/shop")]
        public IActionResult Shop()
        {
            var results = _context.Product
                .OrderBy(p => p.Name);

            return View(results.ToList());
        }
    }
    
}
