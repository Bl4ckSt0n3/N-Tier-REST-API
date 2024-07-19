using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using forceget.DataAccess.Models.Entities;

namespace forceget.DataAccess.Models.DbContexts
{
    public class DimensionsDbContext : DbContext
    {
        public DimensionsDbContext(DbContextOptions<DimensionsDbContext> options) : base(options)
        {
            
        } 
        public DbSet<Dimensions> Dimensions { get; set; } = null!;
    }
}