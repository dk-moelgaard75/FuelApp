using System.Threading.Tasks;
using FuelApp.Models;

namespace FuelApp.Services
{
    public interface IVehicleService
    {
        Task<bool> RegisterVehicle(VehicleModel vehicleModel);
    }
}