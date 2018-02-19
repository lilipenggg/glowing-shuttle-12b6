using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASSIGNMENT8.Controllers
{
    public class ContactUsController : Controller
    {
        private ILogger<ContactUsController> _logger;

        public ContactUsController(ILogger<ContactUsController> logger)
        {
            _logger = logger;
        }
        
        [Route("contactus/index")]
        public IActionResult Index()
        {
            _logger.LogInformation("The Index Action was invoked");
            return View();
        }
    }
}