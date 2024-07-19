using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using forceget.DataAccess.Models.Entities;

namespace forceget.DataAccess.Models.DbContexts
{
    public class OffersDbContext : DbContext
    {
        public OffersDbContext(DbContextOptions<OffersDbContext> options) : base(options)
        {
            
        }    
        public DbSet<Offer> Offers  { get; set; } = null!; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Offer>()
            .ToTable("Offers", "public"); 

            modelBuilder.Entity<Offer>().Property(b => b.Mode);

            modelBuilder.Entity<Offer>().Property(b => b.MovementType);

            modelBuilder.Entity<Offer>().Property(b => b.Incoterms);

            modelBuilder.Entity<Offer>().Property(b => b.CountriesCities);

            modelBuilder.Entity<Offer>().Property(b => b.PackageType);

            modelBuilder.Entity<Offer>().Property(b => b.Unit1);

            modelBuilder.Entity<Offer>().Property(b => b.Unit2);

            modelBuilder.Entity<Offer>().Property(b => b.Currency);

        }
    }
}