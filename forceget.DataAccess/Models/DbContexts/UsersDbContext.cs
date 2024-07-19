using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using forceget.DataAccess.Models.Entities;

namespace forceget.DataAccess.Models.DbContexts
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // IsRequired() method means 'not null'
            // there is no restriction for all entities, only for some of them
                
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>()
            .ToTable("Users", "public"); 

            modelBuilder.Entity<User>().Property(b => b.PasswordHash)
            .IsRequired()
            .HasMaxLength(100);

            modelBuilder.Entity<User>().Property(b => b.PasswordSalt)
            .IsRequired()
            .HasMaxLength(100);

            modelBuilder.Entity<User>().Property(b => b.Age)
            .IsRequired();

            modelBuilder.Entity<User>().Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(b => b.SecondName)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(b => b.CreateDate)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(80);

            modelBuilder.Entity<User>().Property(b => b.Token)
            .IsRequired(); // not null
        }
    }
    
}