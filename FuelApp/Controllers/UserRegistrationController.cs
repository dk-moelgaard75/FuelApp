using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using FuelApp.Services;
using FuelApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FuelApp.Controllers
{
    public class UserRegistrationController : Controller
    {
        private IUserService _userService;
        private IEmailService _emailService;
        private IConfiguration _configuration;
        private IVehicleService _vehicleService;
        public UserRegistrationController(IUserService userService, IConfiguration configuration, 
                        IEmailService emailService, IVehicleService vehicleService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
            _vehicleService = vehicleService;
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
            if (await _userService.IsEmailRegistered(userModel))
            {
                ViewBag.Result = "The email has allready been registrered";
                return View();
            }

            await _userService.RegisterUser(userModel);
            string message = $"Please confirm your email <a href\"http://www.fuelapp.com/UserRegistration/EmailConfirmation/{userModel.GID}\"here</a>";
            await _emailService.SendEmail(userModel.Email, "Welcome to FuelApp", message);

            ViewBag.Result = $"User { userModel.FirstName} {userModel.LastName} has been created - please check the confirmation email"; //TODO: implement mail and append this string  - please check the confirmation email - or confirm <a href=\"/UserRegistration/EmailConfirmation/{userModel.LongId}\">here</a>";
            //During testing or in cases without an emailserver, the AllowQuickEmailConfirmation setting can add a link to the page, 
            //which enables quick emailconfirmation
            if (_configuration["AllowQuickEmailConfirmation"].ToLower() == "true")
            {
                ViewBag.Result  += $" - or confirm <a href=\"/UserRegistration/EmailConfirmation/{userModel.GID}\">here</a>";
            }
            return View();
        }
        [HttpGet("/UserRegistration/EmailConfirmation/{id}", Name = "UserRegistration_confirmation")]
        public async Task<IActionResult> Confirmation(string id)
        {
            //TODO - flag user with ID as confirmed
            ViewBag.Result = "Confirmation failed";
            if (await _userService.ConfirmEmail(id))
            {
                ViewBag.Result = "Confirmation OK";
            }
            else
            {
                ViewBag.Result = "Confirmation not OK";
            }
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
                HttpContext.Session.SetString(SessionUtil.SessionGuidName, curUserModel.GID.ToString());

                //Using the GID from the userModel as a unique identifier.
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,curUserModel.GID.ToString()),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal);
                List<VehicleModel> list = await _vehicleService.GetVehicles(curUserModel.GID.ToString());
                if (list.Count > 0)
                {
                    //Set the session value indicating existing vehicles
                    //Enables menu for adding/editing fueling data
                    HttpContext.Session.SetString(SessionUtil.SessionVehicleSet, "true");
                }

                ViewBag.Result = $"Login OK";
            }
            else
            {
                ViewBag.Result = $"Login FAILED - Please chek username, password and that the user has confirmed the email";
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            //TODO - Get user from DB and print Firstname in logout text
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Result = $"Logout completed";
            return View();
        }
        public IActionResult ErrorDenied()
        {
            return View();
        }

    }
}