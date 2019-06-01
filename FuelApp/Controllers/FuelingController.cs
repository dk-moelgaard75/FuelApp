using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using FuelApp.Services;
using FuelApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FuelApp.Controllers
{
    [Authorize]
    public class FuelingController : Controller
    {
        private IVehicleService _vehicleService;
        private IFuelingService _fuelingService;
        public FuelingController(IVehicleService vehicleService, IFuelingService fuelingService)
        {
            _vehicleService = vehicleService;
            _fuelingService = fuelingService;
        }
        public async Task<IActionResult> Index()
        {
            //for later user:
            //var vehicles = await _context.Vehicles.Where(t => t.UserGID == <Session value for userGID).ToListAsync();
            var userId = HttpContext.Session.GetString(SessionUtil.SessionGuidName);
            List<VehicleModel> vehicles = await _vehicleService.GetVehicles(userId);
            ViewData["Vehicles"] = new SelectList(vehicles, "GID", "GetVehicleIdentification");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(FuelingModel fuelModel)
        {
            //for later user:
            //var vehicles = await _context.Vehicles.Where(t => t.UserGID == <Session value for userGID).ToListAsync();
            var userId = HttpContext.Session.GetString(SessionUtil.SessionGuidName);
            List<VehicleModel> vehicles = await _vehicleService.GetVehicles(userId);
            ViewData["Vehicles"] = new SelectList(vehicles, "GID", "GetVehicleIdentification");
            await _fuelingService.AddFueling(fuelModel);
            ViewBag.Result = $"Fueling registered"; 
            return View();
        }
        
        public async Task<IActionResult> HandleFueling()
        {
            var userId = HttpContext.Session.GetString(SessionUtil.SessionGuidName);
            List<VehicleModel> vehicles = await _vehicleService.GetVehicles(userId);
            List<Guid> guidlist = new List<Guid>();
            foreach (VehicleModel model in vehicles)
            {
                guidlist.Add(model.GID);
            }
            List<FuelingModel> list = await _fuelingService.GetFuelings(guidlist);
            foreach(var fueling in list)
            {
                VehicleModel vehicle = await _vehicleService.GetVehicleByGID(fueling.VehicleGID.ToString());
                //TODO - implement code to delete fueling data, once a vehicle is deleted
                if (vehicle != null)
                {
                    fueling.VehicleName = vehicle.GetVehicleIdentification;
                }
                else
                {
                    fueling.VehicleName = "VEHICLE DELETED";
                }
                
            }
            return View(list);
        }
        public async Task<IActionResult> Edit(int ID)
        {
            //Get vehicleModel from DBContex
            var userId = HttpContext.Session.GetString(SessionUtil.SessionGuidName);
            List<VehicleModel> vehicles = await _vehicleService.GetVehicles(userId);
            ViewData["Vehicles"] = new SelectList(vehicles, "GID", "GetVehicleIdentification");

            FuelingModel fuelingModel = await _fuelingService.GetFuelingByID(ID);
            return View(fuelingModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FuelingModel fuelingModel)
        {
            //Get vehicleModel from DBContex
            await _fuelingService.EditFueling(fuelingModel);
            ViewBag.Result = "Fueling updated";
            return View(fuelingModel);
        }
        public async Task<IActionResult> Delete(int ID)
        {
            await _fuelingService.DeleteFueling(ID);
            return Redirect("/Fueling/HandleFueling");
        }

    }
}