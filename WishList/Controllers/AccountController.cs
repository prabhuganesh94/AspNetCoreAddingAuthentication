﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)                           
                return View("Register", registerViewModel);

           var Result =  _userManager.CreateAsync(new ApplicationUser { UserName = registerViewModel.Email, Email = registerViewModel.Email },registerViewModel.Password);
            if (!Result.Result.Succeeded)
            {
                foreach (var error in Result.Result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                    return View(registerViewModel);
                }
            }
           
            return RedirectToAction("Index", "Home");


        }
    }
}
