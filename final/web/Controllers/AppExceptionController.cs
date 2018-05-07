using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class AppExceptionController : Controller
    {
        /// <summary>
        /// Return a view that tells the user that something went wrong in the system
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}