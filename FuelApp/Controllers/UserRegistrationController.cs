using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using FuelApp.Services;
using FuelApp.Utility;
using Microsoft.AspNetCore.Http;
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
        //Default Index view 
        public IActionResult Index()
        {
            return View();
        }
        
        //This method is called, when user is added
        [HttpPost]
        public async Task<IActionResult> Index(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "An error occured";
                return View();
            }
            //TODO - when email confirmation is in place remove this
            userModel.IsEmailConfirmed = true;

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
            UserModel curUserModel = await _userService.Login(userModel);
            if (curUserModel != null)
            {
                //TODO - set user GID as a sesion variable
                HttpContext.Session.SetString(SessionUtil.SessionGuidName, curUserModel.GID.ToString());
                ViewBag.Result = $"Login OK";
            }
            else
            {
                ViewBag.Result = $"Login FAILED - Please chek username, password and that the user has confirmed the email";
            }
            return View();
        }
        public IActionResult Logout()
        {
            //TODO - Get user from DB and print Firstname in logout text
            HttpContext.Session.Clear();
            ViewBag.Result = $"Logout completed";
            return View();
        }

    }
}