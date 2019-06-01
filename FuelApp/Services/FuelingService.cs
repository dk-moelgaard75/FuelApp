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
        private static DbContextOptions<FuelingDbContext> _dbContextOptions;
        public FuelingService(DbContextOptions<FuelingDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
            FuelingDbContext fuelingDbContext = new FuelingDbContext(_dbContextOptions);
            fuelingDbContext.Database.EnsureCreated();


        }
        public Task<bool> AddFueling(FuelingModel fuelingModel)
        {
            if (fuelingModel.GID == Guid.Empty)
            {
                fuelingModel.GID = Guid.NewGuid();
            }
            FuelingDbContext fuelingDbContext = new FuelingDbContext(_dbContextOptions);
            fuelingDbContext.Add(fuelingModel);
            fuelingDbContext.SaveChanges();

            return Task.FromResult(true);
        }
        public Task<bool> EditFueling(FuelingModel fuelingModel)
        {
            FuelingDbContext dbContext = new FuelingDbContext(_dbContextOptions);

            dbContext.Fuelings.Update(fuelingModel);
            dbContext.SaveChanges();

            return Task.FromResult(true);
        }
        public Task<bool> DeleteFueling(int id)
        {
            FuelingDbContext dbContext = new FuelingDbContext(_dbContextOptions);
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
            FuelingDbContext dbContext = new FuelingDbContext(_dbContextOptions);
            List<FuelingModel> fuelings = dbContext.Fuelings.ToList();

            return Task.FromResult(fuelings);
        }
        public Task<List<FuelingModel>> GetFuelings(List<Guid> list)
        {
            FuelingDbContext dbContext = new FuelingDbContext(_dbContextOptions);
            List<FuelingModel> fuelings = dbContext.Fuelings.Where(f => list.Contains(f.VehicleGID)).ToList();

            return Task.FromResult(fuelings);
        }


        public Task<FuelingModel> GetFuelingByGID(string gid)
        {
            Guid curGID = Guid.Empty;
            Guid.TryParse(gid, out curGID);
            FuelingDbContext dbContext = new FuelingDbContext(_dbContextOptions);
            FuelingModel existingModel = dbContext.Fuelings.FirstOrDefault(u => u.GID == curGID);
            //TODO - check if existingModel is null
            return Task.FromResult(existingModel);
        }
        public Task<FuelingModel> GetFuelingByID(int Id)
        {
            FuelingDbContext dbContext = new FuelingDbContext(_dbContextOptions);
            FuelingModel existingModel = dbContext.Fuelings.FirstOrDefault(u => u.Id == Id);
            //TODO - check if existingModel is null
            return Task.FromResult(existingModel);

        }
    }
}
