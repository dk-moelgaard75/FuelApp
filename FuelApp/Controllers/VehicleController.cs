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
    public class VehicleController : Controller
    {
        private IVehicleService _vehicleService;
        private string _sessionName = "_userGID";
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(VehicleModel vehicleModel)
        {
            string curUserId = HttpContext.Session.GetString(SessionUtil.SessionGuidName);
            Guid guidOutput;
            bool isValid = Guid.TryParse(curUserId, out guidOutput);
            if (!ModelState.IsValid && isValid)
            {
                ViewBag.Result = "An error occured";
                return View();
            }
            vehicleModel.UserGID = guidOutput;
            await _vehicleService.RegisterVehicle(vehicleModel);
            return View();
        }
    }
}