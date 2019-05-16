using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using FuelApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelApp.Controllers
{
    public class UserRegistrationController : Controller
    {
        private IUserService _userService;
        public UserRegistrationController(IUserService userService)
        {
            _userService = userService;
        }
        //Defautl Index view 
        public IActionResult Index()
        {
            return View();
        }
        
        //This method is called, when user is added
        [HttpPost]
        public async Task<IActionResult> Index(UserModel userModel)
        {
            await _userService.RegisterUser(userModel);
            ViewBag.Result = $"User { userModel.FirstName} {userModel.LastName} has been created"; //TODO: implement mail and append this string  - please check the confirmation email - or confirm <a href=\"/UserRegistration/EmailConfirmation/{userModel.LongId}\">here</a>";
            return View();
        }

        //Defautl Login view 
        public IActionResult Login()
        {
            return View();
        }
        //This method is called, when a user is logging in
        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            bool loginOK = await _userService.Login(userModel);
            if (loginOK)
            {
                //TODO - set user GID as a sesion variable
                ViewBag.Result = $"Login OK";
            }
            else
            {
                ViewBag.Result = $"Login FAILED";
            }
            return View();
        }

    }
}