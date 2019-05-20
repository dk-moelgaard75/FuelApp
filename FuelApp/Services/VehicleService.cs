using FuelApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Services
{
    public class VehicleService : IVehicleService
    {
        private static ConcurrentBag<VehicleModel> _vehicleStore;
        public VehicleService()
        {
            _vehicleStore = new ConcurrentBag<VehicleModel>();
        }
        public Task<bool> RegisterVehicle(VehicleModel vehicleModel)
        {
            if (vehicleModel.GID == Guid.Empty)
            {
                vehicleModel.GID = Guid.NewGuid();
            }
            return Task.FromResult(true);
        }
    }
}
