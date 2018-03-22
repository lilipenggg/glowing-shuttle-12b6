using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web.Controllers
{
    public class HomeController : Controller
    {
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
            }
            else
            {
                // Show the error
            }
            
            return View();
        }
    }
    
}
