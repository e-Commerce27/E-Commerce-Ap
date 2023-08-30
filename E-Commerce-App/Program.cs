using E_Commerce_App.Data;
using E_Commerce_App.Models.Interface;
using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.Models.Service;
using E_Commerce_App.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ECommerceContext>(option => option.UseSqlServer(conn));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IProduct, ProductService>();
            builder.Services.AddTransient<ICategory, CategoryService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
