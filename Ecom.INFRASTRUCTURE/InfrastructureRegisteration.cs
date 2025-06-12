using Ecom.CORE.Interfaces;
using Ecom.INFRASTRUCTURE.Data;
using Ecom.INFRASTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
