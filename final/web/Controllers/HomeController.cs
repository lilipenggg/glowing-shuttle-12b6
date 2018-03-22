using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Services;
using web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;

        public HomeController(IMailService mailService)
        {
            _mailService = mailService;
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
    }
    
}
