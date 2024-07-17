using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Extentions;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();





            // App services grouped at custom Extention Method 
            builder.Services.AddAppServices(builder.Configuration);

            #endregion

            var app = builder.Build();


            // Appling Any Pending Migrations To Database Before Run The App 
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _context = services.GetRequiredService<StoreDbContext>();

            var _IdentityContext = services.GetRequiredService<AppIdentityDbContext>();

            var _userManeger = services.GetRequiredService<UserManager<AppUser>>();

            var _signInManeger = services.GetRequiredService<SignInManager<AppUser>>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _context.Database.MigrateAsync(); // Update Database (Business)
                // Data Seeding
                await StoreDbContextSeed.SeedAsync(_context);

                await _IdentityContext.Database.MigrateAsync(); // Update Identity Database
                await AppIdentityDbContextSeed.SeedUsersAsync(_userManeger);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured While appling Migrations");
            }


            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();  // HTTPS Redirection

            app.UseStaticFiles();       // using static files

            app.UseAuthentication();    // Using Authentication

            app.UseAuthorization();     // useing authorization

            app.MapControllers();       // using this middleware to automatic detect the route from controllers

            app.Run();
        }
    }
}
