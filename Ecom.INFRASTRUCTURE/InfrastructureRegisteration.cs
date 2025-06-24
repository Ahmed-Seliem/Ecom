using Ecom.CORE.Interfaces;
using Ecom.CORE.Services;
using Ecom.INFRASTRUCTURE.Data;
using Ecom.INFRASTRUCTURE.Repositories;
using Ecom.INFRASTRUCTURE.Repositories.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.INFRASTRUCTURE
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services , IConfiguration configuration) 
        {
            services.AddScoped(typeof(IGenericRepositiry<>), typeof(GenericRepositiry<>));
            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            //Apply Unit Of Work 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Apply DbContext
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("EcomConnection"));
            });


            return services;
        }
    }
}
