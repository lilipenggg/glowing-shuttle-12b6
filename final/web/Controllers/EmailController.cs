﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Data.Entities;
using web.Models;
using web.Services;
using web.ViewModels;

namespace web.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IKioskRepository _repository;
        private readonly IMailService _mailService;

        public EmailController(IKioskRepository repository, IMailService mailService)
        {
            _repository = repository;
            _mailService = mailService;
        }
        
        /// <summary>
        /// Return a partial view to allow vendor type user to construct a email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create(string count)
        {
            var model = new EmailViewModel
            {
                Email = new EmailModel(),
                Count = count
            };
            
            return View(model);
        }

        /// <summary>
        /// Process and validate the information user provided
        /// if valid, create a email and send it to the recipient
        /// </summary>
        /// <param name="emailViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(EmailViewModel emailViewModel)
        {
            var count = emailViewModel.Count;
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

            emailViewModel.Email.Customers = userModels;
            
            try
            {
                if (ModelState.IsValid)
                {
                    _mailService.SendMail(emailViewModel.Email);
                }
                else
                {
                    return View(emailViewModel);
                }

                return RedirectToAction("List", "Statistic", new {count = emailViewModel.Count});
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(emailViewModel);
            }
        }
    }
}