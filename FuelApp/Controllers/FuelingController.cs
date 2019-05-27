using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using FuelApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FuelApp.Controllers
{
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
            List<VehicleModel> vehicles = await _vehicleService.GetVehicles();
            ViewData["Vehicles"] = new SelectList(vehicles, "GID", "GetVehicleIdentification");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(FuelingModel fuelModel)
        {
            //for later user:
            //var vehicles = await _context.Vehicles.Where(t => t.UserGID == <Session value for userGID).ToListAsync();
            List<VehicleModel> vehicles = await _vehicleService.GetVehicles();
            ViewData["Vehicles"] = new SelectList(vehicles, "GID", "GetVehicleIdentification");
            await _fuelingService.AddFueling(fuelModel);
            ViewBag.Result = $"Fueling registered"; 
            return View();
        }
        
        public async Task<IActionResult> HandleFueling()
        {
            List<FuelingModel> list = await _fuelingService.GetFuelings();
            foreach(var fueling in list)
            {
                VehicleModel vehcile = await _vehicleService.GetVehicleByGID(fueling.VehicleGID.ToString());
                fueling.VehicleName = vehcile.GetVehicleIdentification;
            }
            return View(list);
        }
        public async Task<IActionResult> Edit(string ID)
        {
            //Get vehicleModel from DBContex
            List<VehicleModel> vehicles = await _vehicleService.GetVehicles();
            ViewData["Vehicles"] = new SelectList(vehicles, "GID", "GetVehicleIdentification");

            FuelingModel fuelingModel = await _fuelingService.GetFuelingByGID(ID);
            //            await _vehicleService.RegisterVehicle(vehicleModel);
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
        public async Task<IActionResult> Delete(string ID)
        {
            FuelingModel fuelingModel = await _fuelingService.GetFuelingByGID(ID);
            //await _vehicleService.DeleteVehicle(vehicleModel);
            await _fuelingService.DeleteFueling(fuelingModel);

            return Redirect("/Fueling/HandleFueling");
        }

    }
}