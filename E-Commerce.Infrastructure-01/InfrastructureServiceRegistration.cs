using E_Commerce.Application_01.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure_01.Data;
using E_Commerce.Infrastructure_01.Repositories;
using E_Commerce.Infrastructure_01.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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



            return services;
        }
    }
}
