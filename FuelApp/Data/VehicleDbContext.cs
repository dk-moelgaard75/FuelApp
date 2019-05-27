using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelApp.Data
{
    public class VehicleDbContext : DbContext
    {
        public DbSet<VehicleModel> Users { get; set; }
        public VehicleDbContext(DbContextOptions<VehicleDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("vehicles");
        }
    }
}
