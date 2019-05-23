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
        private static List<VehicleModel> _vehicleStore;
        
        public VehicleService()
        {
            _vehicleStore = new List<VehicleModel>();
        }
        public Task<List<VehicleModel>> GetVehicles()
        {
            return Task.FromResult(_vehicleStore.ToList());
        }
        public Task<VehicleModel> GetVehicleByGID(string gid)
        {
            Guid curGID = Guid.Empty;
            Guid.TryParse(gid, out curGID);
            return Task.FromResult(_vehicleStore.FirstOrDefault(u => u.GID == curGID));
        }
        public Task<bool> RegisterVehicle(VehicleModel vehicleModel)
        {
            if (vehicleModel.GID == Guid.Empty)
            {
                vehicleModel.GID = Guid.NewGuid();
            }
            _vehicleStore.Add(vehicleModel);
            return Task.FromResult(true);
        }
        public Task<bool> UpdateVehicle(VehicleModel vehicleModel)
        {
            var itemIndex = _vehicleStore.FindIndex(x => x.GID == vehicleModel.GID);
            _vehicleStore[itemIndex] = vehicleModel;
            return Task.FromResult(true);
        }
        public Task<bool> DeleteVehicle(VehicleModel vehicleModel)
        {
            var itemIndex = _vehicleStore.FindIndex(x => x.GID == vehicleModel.GID);
            _vehicleStore.RemoveAt(itemIndex);
            return Task.FromResult(true);
        }

    }
}
