using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace web.Controllers
{
    public class OrderController : Controller
    {
        // GET
        public async Task<IActionResult> Checkout()
        {
            return View();
        }
    }
}