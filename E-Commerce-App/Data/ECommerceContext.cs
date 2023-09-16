using E_Commerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Data
{   
    public class ECommerceContext : IdentityDbContext<AuthUser>
    {
       public ECommerceContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole { Id = "administrator", Name = "Administrator", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = Guid.Empty.ToString()},
             new IdentityRole { Id = "editor", Name = "Editor", NormalizedName = "EDITOR", ConcurrencyStamp = Guid.Empty.ToString()}



   );

            modelBuilder.Entity<Product>().HasData(

             new Product { Id = 1, Name = "Tomato", Price = 2, Description = "item1", CategoryId=1 ,Image=""},
             new Product { Id = 2, Name = "Tuna",  Price = 5, Description = "item2" ,CategoryId=2, Image = "" },
             new Product { Id = 3, Name = "steak",  Price = 30, Description = "item3",CategoryId =3 , Image = "" }

         );

      
            modelBuilder.Entity<Category>().HasData(

             new Category { Id = 1, Name = "Fruits and vegetables",Img= "https://c4.wallpaperflare.com/wallpaper/311/699/596/fruit-allsorts-pineapple-melon-wallpaper-preview.jpg", Type = "category1" },
             new Category { Id = 2, Name = "Detergents section", Img= "https://w7.pngwing.com/pngs/163/54/png-transparent-ariel-laundry-detergent-persil-ariel-laundry-detergent-with-downy-cleaning-stain-detergent-thumbnail.png", Type = "category2" },
             new Category { Id = 3, Name = "Fresh Meat and Fresh Chicken ",Img= "https://c4.wallpaperflare.com/wallpaper/304/644/960/chicken-dishes-table-plate-fruit-vegetables-wallpaper-preview.jpg", Type = "category3" }

         );

      


        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Prodects { get; set; }


    }
}
