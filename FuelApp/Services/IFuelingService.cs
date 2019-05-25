using System.Collections.Generic;
using System.Threading.Tasks;
using FuelApp.Models;

namespace FuelApp.Services
{
    public interface IFuelingService
    {
        Task<bool> AddFueling(FuelingModel fuelingModel);
        Task<bool> DeleteFueling(FuelingModel fuelingModel);
        Task<bool> EditFueling(FuelingModel fuelingModel);
        Task<List<FuelingModel>> GetFuelings();
        Task<FuelingModel> GetFuelingByGID(string gid);
    }
}