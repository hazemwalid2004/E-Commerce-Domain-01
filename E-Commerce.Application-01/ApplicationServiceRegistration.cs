using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application_01.Contracts;
using E_Commerce.Application_01.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Application_01
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationServicesRegisteration).Assembly);
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
