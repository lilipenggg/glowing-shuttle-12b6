using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web.ViewModels;
using web.Enums;
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

        /// <summary>
        /// Return a view for user to login
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Allow user to login to the applcation based on the entered username and password
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// LoginRedirect is called when customer selects to sign in before checkout
        /// and it will return a view for user to enter login information
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult LoginRedirect(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Process the login information and redirect user back to shopping cart checkout page
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return a view for user to enter registration information
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Process and validate the registration information that user filled out,
        /// if all the information is valid, it will create a new user in the system
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allow user to logout from the system
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Return a view of the user's information that he/she can edit
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Process and validate the information that user filled out to udpate their own information
        /// if all is valid, update the current user's info within the database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="applicationUserModel"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Return a list of all the application users to the admin management page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> List()
        {
            if (!User.IsInRole(RoleType.Employee.Value))
            {
                return NotFound();
            }
            
            var userModels = (await _repository.GetApplicationUsers()).Select(u => new ApplicationUserModel
            {
                ApplicatinUserUserName = u.UserName,
                ApplicationUserEmail = u.ApplicationUserEmail,
                ApplicationUserAwardPoints = u.ApplicationUserAwardPoints,
                ApplicationUserFirstName = u.ApplicationUserFirstName,
                ApplicationUserLastName = u.ApplicationUserLastName,
                ApplicationUserPhoneNumber = u.ApplicationUserPhoneNumber
            }).ToList();
            
            return View(userModels);
        }
        
        /// <summary>
        /// Return a view for admin to create a new user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _repository.GetAllRoles();
            var model = new RegisterViewModel
            {
                ApplicationUser = new ApplicationUserModel(),
                UserRoleModels = roles
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
        
        /// <summary>
        /// Process and validate the information that admin provides
        /// if information is valid, then create a new user in the system
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel registerViewModel)
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
        
    }
}