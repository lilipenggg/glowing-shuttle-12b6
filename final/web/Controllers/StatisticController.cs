using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Data.Entities;
using web.Models;
using web.Services;
using web.ViewModels;

namespace web.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IKioskRepository _repository;

        public StatisticController(IKioskRepository repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Return a view of a list of customer users who have purchased this vendor's product within the last month
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> List(string count)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(count))
            {
                users = await _repository.GetApplicationUserPurchasedLastMonth(User.Identity.Name);
            }
            else
            {
                var numOfPurchased = int.Parse(count);
                users = await _repository.GetApplicationUserPurchasedNumOfTimes(User.Identity.Name, numOfPurchased);
            }
            

            var userModels = users.Select(u => new ApplicationUserModel
            {
                ApplicatinUserUserName = u.UserName,
                ApplicationUserEmail = u.ApplicationUserEmail,
                ApplicationUserAwardPoints = u.ApplicationUserAwardPoints,
                ApplicationUserFirstName = u.ApplicationUserFirstName,
                ApplicationUserLastName = u.ApplicationUserLastName,
                ApplicationUserPhoneNumber = u.ApplicationUserPhoneNumber
            }).ToList();

            var model = new StatisticViewModel
            {
                NumOfPurchases = count,
                Users = userModels
            };
            
            return View(model);
        }
    }
}