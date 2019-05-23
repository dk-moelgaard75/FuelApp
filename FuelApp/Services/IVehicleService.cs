using System.Threading.Tasks;
using FuelApp.Models;
using System.Collections.Generic;

namespace FuelApp.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleModel>> GetVehicles();
        Task<bool> RegisterVehicle(VehicleModel vehicleModel);
        Task<bool> UpdateVehicle(VehicleModel vehicleModel);
        Task<bool> DeleteVehicle(VehicleModel vehicleModel);
        //until DBContex has been implemented use this to get existing vehicles
        Task<VehicleModel> GetVehicleByGID(string gid);
    }
}