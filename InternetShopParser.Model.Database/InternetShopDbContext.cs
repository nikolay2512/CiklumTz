using System;
using InternetShopParser.Model.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopParser.Model.Database
{
    public class InternetShopDbContext : DbContext
    {
        public InternetShopDbContext(DbContextOptions<InternetShopDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUpdetePrice> ProductUpdetePrices { get; set; }
    }
}
