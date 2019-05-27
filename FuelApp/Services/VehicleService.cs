using FuelApp.Data;
using FuelApp.Models;
using Microsoft.EntityFrameworkCore;
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
        private static DbContextOptions<VehicleDbContext> _dbContextOptions;
        public VehicleService(DbContextOptions<VehicleDbContext> dbContextOptions)
        {
            _vehicleStore = new List<VehicleModel>();
            _dbContextOptions = dbContextOptions;
            VehicleDbContext _vehicleDbContext = new VehicleDbContext(_dbContextOptions);
            _vehicleDbContext.Database.EnsureCreated();


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
