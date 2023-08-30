using E_Commerce_App.Models;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Product>().HasData(

             new Product { Id = 1, Name = "Name1", Price = 5, Description = "item1", CategoryId=1 },
             new Product { Id = 2, Name = "Name2",  Price = 10, Description = "item2" ,CategoryId=2},
             new Product { Id = 3, Name = "Name3 ",  Price = 15, Description = "item3",CategoryId =3 }

         );

      
            modelBuilder.Entity<Category>().HasData(

             new Category { Id = 1, Name = "Name1",Type = "category1" },
             new Category { Id = 2, Name = "Name2", Type = "category2" },
             new Category { Id = 3, Name = "Name3 ", Type = "category3" }

         );

      


        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Prodects { get; set; }


    }
}
