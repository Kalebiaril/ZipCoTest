using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.WebAPI.Data.Db
{
    public class ZipPayDbContext : DbContext
    {
        /// <summary>
        /// Collection of the all users
        /// </summary>
        public DbSet<User> Users { get; set; } 
       
        /// <summary>
        /// Collection of the all accounts
        /// </summary>
        public DbSet<Account> Accounts { get; set; }
        
        public ZipPayDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                  .HasIndex(u => u.Email)
                  .IsUnique();

            modelBuilder.Entity<Account>()
                  .HasOne(b => b.User)
                  .WithOne();

            base.OnModelCreating(modelBuilder);
        }
    }
}
