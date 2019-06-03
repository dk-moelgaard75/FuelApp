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
        
        private static DbContextOptions<FuelAppDbContext> _dbAppContextOptions;
        public UserService(DbContextOptions<FuelAppDbContext> dbAppContextOptions)
        {
            //Since userservice is the first service accessed it´s the place to ensure DB creation
            _dbAppContextOptions = dbAppContextOptions;
            FuelAppDbContext fuelAppDbContext = new FuelAppDbContext(_dbAppContextOptions);
            fuelAppDbContext.Database.EnsureCreated();
        }

        public Task<bool> RegisterUser(UserModel userModel)
        {
            //check for empty GUID and create one if empty
            if (userModel.GID == Guid.Empty)
            {
                userModel.GID = Guid.NewGuid();
            }
            FuelAppDbContext appDbContext = new FuelAppDbContext(_dbAppContextOptions);
            appDbContext.Users.Add(userModel);
            appDbContext.SaveChanges();
            return Task.FromResult(true);
        }
        public Task<UserModel> Login(UserModel userModel)
        {
            //TODO - check email,password and confirmation with DB
            //UserDbContext _userDbContext = new UserDbContext(_dbContextOptions);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            UserModel existingUser = dbContext.Users.FirstOrDefault(u => u.Email == userModel.Email && u.Password == userModel.Password && u.IsEmailConfirmed);
            return Task.FromResult(existingUser);
        }
        public Task<bool> IsEmailRegistered(UserModel userModel)
        {
            //UserDbContext _userDbContext = new UserDbContext(_dbContextOptions);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            UserModel existingUser = dbContext.Users.FirstOrDefault(u => u.Email == userModel.Email);
            return Task.FromResult(existingUser != null ? true : false);

        }
        public Task<bool> ConfirmEmail(string id)
        {
            Guid guid = Guid.Parse(id);
            //UserDbContext userDbContext = new UserDbContext(_dbContextOptions);
            FuelAppDbContext dbContext = new FuelAppDbContext(_dbAppContextOptions);
            UserModel existingUser = dbContext.Users.FirstOrDefault(u => u.GID == guid);

            if (existingUser != null)
            {
                existingUser.IsEmailConfirmed = true;
                existingUser.EmailConfirmationDate = DateTime.Now;
                dbContext.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

    }
}
