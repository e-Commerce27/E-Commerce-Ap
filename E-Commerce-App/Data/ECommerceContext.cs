using E_Commerce_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace E_Commerce_App.Data
{
    public class ECommerceContext : DbContext
    {
      public ECommerceContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> prodects { get; set; }
    }
}






