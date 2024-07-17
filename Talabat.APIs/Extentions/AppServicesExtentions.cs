using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Services.Interfaces;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Repositories;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
    public static class AppServicesExtentions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {

            // DbContexts configurations and Services
            services.AddDbContext<StoreDbContext>(options =>  // DbContext DI
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<AppIdentityDbContext>(options =>      // Identity DbCOntext DI
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            // redis Service
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>  // Redis Services DI 
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            // Dependancy Injection

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic repo DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();


            //Identity Configurations and DI
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            // Mapper
            services.AddAutoMapper(typeof(MappingProfile));  // AutoMapper Profile DI

            //JWt Authntecation Services
            services.AddAuthentication(AuthOptions =>
            {
                // Set Bearer As Default Schema
                AuthOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                AuthOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(bearerOptions =>  // Token Validation Check
                    {
                        bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["JWT:ValidIssuer"],
                            ValidateAudience = true,
                            ValidAudience = configuration["JWT:ValidAudience"],
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                        };
                    });  //


            // Configure Api validation Error Response 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Values.Where(V => V.Errors.Count > 0)
                                                                .SelectMany(E => E.Errors)
                                                                .Select(E => E.ErrorMessage).ToArray();

                    var validationResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationResponse);
                };
            });

            return services;
        }

    }
}
