using FuelApp.Data;
using FuelApp.Models;
using Microsoft.AspNetCore.Http;
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
        //private static DbContextOptions<VehicleDbContext> _dbContextOptions;
        private static DbContextOptions<FuelAppDbContext> _dbAppContextOptions;
        public VehicleService(DbContextOptions<FuelAppDbContext> dbContextOptions)
        {
            _dbAppContextOptions = dbContextOptions;
            //VehicleDbContext vehicleDbContext = new VehicleDbContext(_dbContextOptions);
            //vehicleDbContext.Database.EnsureCreated();
            

        }
        public Task<List<VehicleModel>> GetVehicles(string userId)
        {
            //Get user GID from session
            Guid curGID = Guid.Empty;
            Guid.TryParse(userId, out curGID);

            FuelAppDbContext dbContext  = new FuelAppDbContext(_dbAppContextOptions);

            List<VehicleModel> list = dbContext.Vehicles.Where(v => v.UserGID == curGID).ToList();
            return Task.FromResult(list);
        }
        public Task<VehicleModel> GetVehicleByGID(string gid)
        {

            Guid curGID = Guid.Empty;
            Guid.TryParse(gid, out curGID);
            //VehicleDbContext dbContext = new VehicleDbContext(_dbContextOptions);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            VehicleModel existingModel = dbContext.Vehicles.FirstOrDefault(u => u.GID == curGID);

            return Task.FromResult(existingModel);

        }
        public Task<VehicleModel> GetVehicleByID(int id) //string gid)
        {
            /*
            Guid curGID = Guid.Empty;
            Guid.TryParse(gid, out curGID);
            */
            //VehicleDbContext dbContext = new VehicleDbContext(_dbContextOptions);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            VehicleModel existingModel = dbContext.Vehicles.FirstOrDefault(u => u.Id == id);

            return Task.FromResult(existingModel);
        }
        public Task<bool> RegisterVehicle(VehicleModel vehicleModel)
        {
            if (vehicleModel.GID == Guid.Empty)
            {
                vehicleModel.GID = Guid.NewGuid();
            }
            //_vehicleStore.Add(vehicleModel);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            //VehicleDbContext vehicleDbContext = new VehicleDbContext(_dbContextOptions);
            dbContext.Vehicles.Add(vehicleModel);
            dbContext.SaveChanges();

            return Task.FromResult(true);
        }
        public Task<bool> UpdateVehicle(VehicleModel vehicleModel)
        {
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            dbContext.Vehicles.Update(vehicleModel);
            dbContext.SaveChanges();
            return Task.FromResult(true);
        }
        public Task<bool> DeleteVehicle(int id) //VehicleModel vehicleModel)
        {
            //VehicleDbContext dbContext = new VehicleDbContext(_dbContextOptions);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            VehicleModel existingModel = dbContext.Vehicles.FirstOrDefault(u => u.Id == id);

            if (existingModel != null)
            {
                dbContext.Remove(existingModel);
                dbContext.SaveChanges();
            }
            return Task.FromResult(true);
        }

    }
}
