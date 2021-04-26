using Microsoft.EntityFrameworkCore;
using Stolice.Database.Models;
using System;

namespace Stolice.Database
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<Capital> Capitals { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
