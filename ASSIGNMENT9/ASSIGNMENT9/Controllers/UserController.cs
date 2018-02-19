using System.Collections.Generic;
using ASSIGNMENT9.Interfaces;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASSIGNMENT9.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserStore _userStore;

        public UserController(IUserStore userStore)
        {
            _userStore = userStore;
        }

        [HttpGet("[action]")]
        public IActionResult Test()
        {
            return Ok("worked");
        }

        [HttpGet()]
        public IList<string> List()
        {
            return _userStore.ListAll();
        }

        [HttpPost()]
        public IActionResult Create(string name)
        {
            _userStore.CreateUser(name);
            return Ok("User created");
        }
    }
}