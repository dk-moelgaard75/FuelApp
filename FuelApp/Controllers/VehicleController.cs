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
            ViewBag.Result = "Vehicle created";
            return View();
        }
        public async Task<IActionResult> HandleVehicle()
        {
            return View(await _vehicleService.GetVehicles());
        }

        public async Task<IActionResult> Edit(string ID)
        {
            //Get vehicleModel from DBContex

            VehicleModel vehicleModel = await _vehicleService.GetVehicleByGID(ID);
//            await _vehicleService.RegisterVehicle(vehicleModel);
            return View(vehicleModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(VehicleModel vehicleModel)
        {
            await _vehicleService.UpdateVehicle(vehicleModel);
            ViewBag.Result = "Vehicle updated";
            return View(vehicleModel);
        }
        public async void Delete(string ID)
        {
            VehicleModel vehicleModel = await _vehicleService.GetVehicleByGID(ID);
            await _vehicleService.DeleteVehicle(vehicleModel);
            //TODO - why does this fails?
            RedirectToAction("Vehicle", "Edit");
        }

    }
}