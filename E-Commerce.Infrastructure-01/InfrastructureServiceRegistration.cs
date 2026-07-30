using E_Commerce.Application_01.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure_01.Data;
using E_Commerce.Infrastructure_01.Identity.Data;
using E_Commerce.Infrastructure_01.Identity.Entity;
using E_Commerce.Infrastructure_01.Identity.Services;
using E_Commerce.Infrastructure_01.Repositories;
using E_Commerce.Infrastructure_01.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Infrastructure_01
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Redis

            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                return ConnectionMultiplexer.Connect(
                    configuration.GetConnectionString("RedisConnection")!);
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICasheRepository, CasheRepository>();



            #endregion

            #region Identity

            services.AddDbContext<StoreIdentityDbContexts>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentityCore<ApplicationUser>()
           .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContexts>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            #region Token

            var jwtSettings = configuration.GetSection("JWT").Get<JWTSettings>()
                ?? throw new InvalidOperationException("JWT Settings Is Not Configured");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = true;

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,

                    ClockSkew = TimeSpan.Zero,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });


            #endregion

            #endregion
            return services;
        }
    }
}
