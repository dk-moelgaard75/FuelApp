using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Models;
using Microsoft.EntityFrameworkCore;
namespace FuelApp.Data
{
    public class FuelingDbContext : DbContext
    {
        public DbSet<FuelingModel> Gueling { get; set; }
        public FuelingDbContext(DbContextOptions<FuelingDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("fueling");
        }
    }
}
