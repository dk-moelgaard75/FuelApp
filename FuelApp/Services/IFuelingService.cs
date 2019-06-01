using System.Collections.Generic;
using System.Threading.Tasks;
using FuelApp.Models;
using System;

namespace FuelApp.Services
{
    public interface IFuelingService
    {
        Task<bool> AddFueling(FuelingModel fuelingModel);
        Task<bool> DeleteFueling(int id);
        Task<bool> EditFueling(FuelingModel fuelingModel);
        Task<List<FuelingModel>> GetFuelings();
        Task<List<FuelingModel>> GetFuelings(List<Guid> list);
        Task<FuelingModel> GetFuelingByGID(string gid);
        Task<FuelingModel> GetFuelingByID(int id);
    }
}