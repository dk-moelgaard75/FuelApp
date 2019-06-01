using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using Microsoft.EntityFrameworkCore;
namespace FuelApp.Data
{
    public class FuelAppDbContext : DbContext
    {
        public DbSet<FuelingModel> Fuelings { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<VehicleModel> Vehicles { get; set; }

        public FuelAppDbContext(DbContextOptions<FuelAppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<VehicleModel>().ToTable("vehicles");
            modelBuilder.Entity<FuelingModel>().ToTable("fueling");
        }
    }
}
