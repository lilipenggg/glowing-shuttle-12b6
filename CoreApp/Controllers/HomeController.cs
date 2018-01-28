using Microsoft.AspNetCore.Mvc;

namespace CoreApp
{
    public class HomeController : Controller
    {
        [Route("home/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("home/index/{username?}")]
        public IActionResult Index(string username ="you")
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