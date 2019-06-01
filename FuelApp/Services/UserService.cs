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
    public class UserService : IUserService
    {
        
        private static DbContextOptions<UserDbContext> _dbContextOptions;
        private static DbContextOptions<FuelAppDbContext> _dbAppContextOptions;
        public UserService(DbContextOptions<UserDbContext> dbContextOptions, DbContextOptions<FuelAppDbContext> dbAppContextOptions)
        {
            _dbContextOptions = dbContextOptions;
            _dbAppContextOptions = dbAppContextOptions;
            //UserDbContext userDbContext = new UserDbContext(_dbContextOptions);
            //userDbContext.Database.EnsureCreated();
            FuelAppDbContext fuelAppDbContext = new FuelAppDbContext(_dbAppContextOptions);
            fuelAppDbContext.Database.EnsureCreated();
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
            //_userStore.Add(userModel);

            //UserDbContext userDbContext = new UserDbContext(_dbAppContextOptions);
            //userDbContext.Add(userModel);
            //userDbContext.
            FuelAppDbContext appDbContext = new FuelAppDbContext(_dbAppContextOptions);
            appDbContext.Users.Add(userModel);
            appDbContext.SaveChanges();
            return Task.FromResult(true);
        }
        public Task<UserModel> Login(UserModel userModel)
        {
            //TODO - check email,password and confirmation with DB
            UserDbContext _userDbContext = new UserDbContext(_dbContextOptions);
            UserModel existingUser = _userDbContext.Users.FirstOrDefault(u => u.Email == userModel.Email && u.Password == userModel.Password && u.IsEmailConfirmed);
            return Task.FromResult(existingUser);
        }
        public Task<bool> IsEmailRegistered(UserModel userModel)
        {
            UserDbContext _userDbContext = new UserDbContext(_dbContextOptions);
            UserModel existingUser = _userDbContext.Users.FirstOrDefault(u => u.Email == userModel.Email);
            return Task.FromResult(existingUser != null ? true : false);

        }
        public Task<bool> ConfirmEmail(string id)
        {
            Guid guid = Guid.Parse(id);
            UserDbContext userDbContext = new UserDbContext(_dbContextOptions);
            UserModel existingUser = userDbContext.Users.FirstOrDefault(u => u.GID == guid);

            if (existingUser != null)
            {
                existingUser.IsEmailConfirmed = true;
                existingUser.EmailConfirmationDate = DateTime.Now;
                userDbContext.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

    }
}
