using FuelApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Services
{
    public class UserService : IUserService
    {
        private static ConcurrentBag<UserModel> _userStore;
        public UserService()
        {
            _userStore = new ConcurrentBag<UserModel>();
        }
        public Task<bool> RegisterUser(UserModel userModel)
        {
            //TODO - check for empty GUID and create one if empty
            if (userModel.GID == Guid.Empty)
            {
                userModel.GID = Guid.NewGuid();
            }
            //TODO - add user to DB
            //Temporary add user to ConcurrentBag
            _userStore.Add(userModel);
            return Task.FromResult(true);
        }
        public Task<bool> Login(UserModel userModel)
        {
            //TODO - check email and password with DB
            return Task.FromResult(true);
        }
    }
}
