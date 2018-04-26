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
                    // Redirect to the checkout page once the user signed in
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("CheckoutSignedIn", "Order");

                    return RedirectToAction(loginViewModel.ReturnUrl);
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
                    // Assign the selected role the new user
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

        [HttpGet]
        public async Task<IActionResult> Edit(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userModel = new ApplicationUserModel
            {
                ApplicatinUserUserName = user.UserName,
                ApplicationUserAwardPoints = user.ApplicationUserAwardPoints,
                ApplicationUserEmail = user.ApplicationUserEmail,
                ApplicationUserFirstName = user.ApplicationUserFirstName,
                ApplicationUserLastName = user.ApplicationUserLastName,
                ApplicationUserPhoneNumber = user.ApplicationUserPhoneNumber
            };
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userName, ApplicationUserModel applicationUserModel)
        {
            var user = await _userManager.FindByNameAsync(userName);
            
            if (ModelState.IsValid)
            {
                user.ApplicationUserFirstName = applicationUserModel.ApplicationUserFirstName;
                user.ApplicationUserLastName = applicationUserModel.ApplicationUserLastName;
                user.ApplicationUserEmail = applicationUserModel.ApplicationUserEmail;
                user.ApplicationUserPhoneNumber = applicationUserModel.ApplicationUserPhoneNumber;
                await _userManager.UpdateAsync(user);
            }

            return View(applicationUserModel);
        }
        
        // TODO: Need to create one action that return a overview of the list of registered users
        /*
        public async Task<IActionResult> List()
        {
            return View();
        }
        
        // TODO: Need to create one action HttpGet that return a Create User view
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        
        // TODO: Need to create one action HttpPost that can process the creation of a user
        // Might be able to reuse the database query that register a new user for this action
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel registerViewModel)
        {
            return View();
        }
        */
    }
}