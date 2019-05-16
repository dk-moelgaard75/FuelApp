using FuelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Services
{
    public class UserService : IUserService, IUserService1
    {
        public Task<bool> RegisterUser(UserModel userModel)
        {
            return Task.FromResult(true);
        }
        public Task<bool> Login(UserModel userModel)
        {
            //TODO - check email and password with DB
            return Task.FromResult(true);
        }
    }
}
