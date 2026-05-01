using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Services;

namespace Overlapssystem.Application
{
    public static class DependencyInjection
    {
            // Registers application services in the DI container. 
            public static IServiceCollection AddApplication(this IServiceCollection services)
            {
            services.AddScoped<IEmployeeCreationService, EmployeeCreationService>();
            services.AddScoped<IResidentSchemaService, ResidentSchemaService>();
            services.AddScoped<IResidentStatusService, ResidentStatusService>();
            // If there are more services, register them here. 

            return services;
        }
    }
}
