using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using web.Data.Entities;
using web.Models;
using web.Services;

namespace web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Data.Entities.ApplicationUser> _userManager;
        private readonly SignInManager<Data.Entities.ApplicationUser> _signInManager;
        private readonly IKioskRepository _repository;

        public AccountController(UserManager<Data.Entities.ApplicationUser> userManager,
            SignInManager<Data.Entities.ApplicationUser> signInManager, IKioskRepository repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Username/password not found");
            return View(loginViewModel);
        }
        
        [AllowAnonymous]
        public IActionResult LoginRedirect(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginRedirect(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return RedirectToAction("Index", "ShoppingCart");
                }
            }

            ModelState.AddModelError("", "Username/password not found");
            return View(loginViewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {   
            var roles = await _repository.GetAllRoles();
            var model = new RegisterViewModel
            {
                ApplicationUser = new ApplicationUserModel(),
                UserRoleModels = roles
                    .Where(r => r.NormalizedName != "EMPLOYEE")
                    .Select(r => new UserRoleModel
                    {
                        UserRoleId = r.Id,
                        UserRoleName = r.Name
                    })
                    .OrderBy(r => r.UserRoleName)
                    .ToList()
            };
            
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var applicationUserModel = registerViewModel.ApplicationUser;
            
            if (ModelState.IsValid)
            {
                var user = new Data.Entities.ApplicationUser()
                {
                    UserName = applicationUserModel.ApplicatinUserUserName,
                    ApplicationUserAwardPoints = 0,
                    ApplicationUserEmail = applicationUserModel.ApplicationUserEmail,
                    ApplicationUserFirstName = applicationUserModel.ApplicationUserFirstName,
                    ApplicationUserLastName = applicationUserModel.ApplicationUserLastName,
                    ApplicationUserPhoneNumber = applicationUserModel.ApplicationUserPhoneNumber,
                    Email = applicationUserModel.ApplicationUserEmail
                };

                var result = await _userManager.CreateAsync(user, applicationUserModel.ApplicationUserPassword);

                if (result.Succeeded)
                {
                    // assign the selected role the new user
                    result = await _userManager.AddToRoleAsync(user,
                        applicationUserModel.ApplicationUserRole.UserRoleName);
                    
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}