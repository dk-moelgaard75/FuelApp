using FuelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Services
{
    public class FuelingService : IFuelingService
    {
        private static List<FuelingModel> _fuelingStore;
        public FuelingService()
        {
            _fuelingStore = new List<FuelingModel>();
        }
        public Task<bool> AddFueling(FuelingModel fuelingModel)
        {
            if (fuelingModel.GID == Guid.Empty)
            {
                fuelingModel.GID = Guid.NewGuid();
            }
            _fuelingStore.Add(fuelingModel);
            return Task.FromResult(true);
        }
        public Task<bool> EditFueling(FuelingModel fuelingModel)
        {
            var itemIndex = _fuelingStore.FindIndex(x => x.GID == fuelingModel.GID);
            _fuelingStore[itemIndex] = fuelingModel;
            return Task.FromResult(true);
        }
        public Task<bool> DeleteFueling(FuelingModel fuelingModel)
        {
            var itemIndex = _fuelingStore.FindIndex(x => x.GID == fuelingModel.GID);
            _fuelingStore.RemoveAt(itemIndex);
            return Task.FromResult(true);
        }
        public Task<List<FuelingModel>> GetFuelings()
        {
            return Task.FromResult(_fuelingStore.ToList());
        }

        public Task<FuelingModel> GetFuelingByGID(string gid)
        {
            Guid curGID = Guid.Empty;
            Guid.TryParse(gid, out curGID);
            return Task.FromResult(_fuelingStore.FirstOrDefault(u => u.GID == curGID));
        }
    }
}
