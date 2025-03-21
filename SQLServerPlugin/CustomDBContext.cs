using CoreBusiness.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SQLServerPlugin
{
    public class CustomDBContext : DbContext
    {
        public CustomDBContext(DbContextOptions<CustomDBContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.Role);
        }
    }
}
