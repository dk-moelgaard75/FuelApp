using FuelApp.Data;
using FuelApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Services
{
    public class FuelingService : IFuelingService
    {
        private static DbContextOptions<FuelAppDbContext> _dbContextOptions;
        public FuelingService(DbContextOptions<FuelAppDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public Task<bool> AddFueling(FuelingModel fuelingModel)
        {
            if (fuelingModel.GID == Guid.Empty)
            {
                fuelingModel.GID = Guid.NewGuid();
            }
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbContextOptions);
            dbContext.Add(fuelingModel);
            dbContext.SaveChanges();

            return Task.FromResult(true);
        }
        public Task<bool> EditFueling(FuelingModel fuelingModel)
        {
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbContextOptions);

            dbContext.Fuelings.Update(fuelingModel);
            dbContext.SaveChanges();

            return Task.FromResult(true);
        }
        public Task<bool> DeleteFueling(int id)
        {
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbContextOptions);
            FuelingModel existingModel = dbContext.Fuelings.FirstOrDefault(u => u.Id == id);
            if (existingModel != null)
            {
                dbContext.Fuelings.Remove(existingModel);
                dbContext.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
        
        public Task<List<FuelingModel>> GetFuelings()
        {
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbContextOptions);
            List<FuelingModel> fuelings = dbContext.Fuelings.ToList();
            return Task.FromResult(fuelings);
        }
        public Task<List<FuelingModel>> GetFuelings(List<Guid> list)
        {
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbContextOptions);
            List<FuelingModel> fuelings = dbContext.Fuelings.Where(f => list.Contains(f.VehicleGID)).ToList();
            return Task.FromResult(fuelings);
        }


        public Task<FuelingModel> GetFuelingByGID(string gid)
        {
            Guid curGID = Guid.Empty;
            Guid.TryParse(gid, out curGID);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbContextOptions);
            FuelingModel existingModel = dbContext.Fuelings.FirstOrDefault(u => u.GID == curGID);
            return Task.FromResult(existingModel);
        }
        public Task<FuelingModel> GetFuelingByID(int Id)
        {
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbContextOptions);
            FuelingModel existingModel = dbContext.Fuelings.FirstOrDefault(u => u.Id == Id);
            return Task.FromResult(existingModel);

        }
    }
}
