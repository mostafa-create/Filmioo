using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Filmioo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<FilmiooDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(M => M.AddProfiles(new List<Profile>() { new MovieProfile(), new ActorProfile(), new GenreProfile(), new ReviewProfile()}));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true; // @ #
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;

            })
                .AddEntityFrameworkStores<FilmiooDBContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(Options =>
                {
                    Options.LoginPath = "Account/Login";
                    Options.AccessDeniedPath = "Home/Error";
                });
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movie}/{action=Movies}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    Seeding.SeedRolesAndAdminAsync(services).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            app.Run();
        }
    }
}
