using Microsoft.AspNetCore.Mvc;

namespace ASSIGNMENT5.Controllers
{
    public class HomeController : Controller
    {
        [Route("home/index/{username?}")]
        public IActionResult Index(string username ="beautilful")
        {
            return View(new Greeting { Username = username });
        }

        [Route("home/greet/{username}")]
        public IActionResult Greet(string username)
        {
            var greeting = new Greeting { Username = username };

            return Ok(greeting);
        }
    }

    public class Greeting {
        public string Username { get; set; }
    }
}