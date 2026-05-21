using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Services;
using Overlapssystem.Application.UseCases;

namespace Overlapssystem.Application
{
    public static class DependencyInjection
    {
            // Registers application services in the DI container. 
            public static IServiceCollection AddApplication(this IServiceCollection services)
            {
            services.AddScoped<IResidentSchemaService, ResidentSchemaService>();
            services.AddScoped<IResidentStatusService, ResidentStatusService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<ITrafficLightService, TrafficLightService>();
            services.AddScoped<IMedicationService, MedicationService>();
            services.AddScoped<IPNService, PNService>();
            services.AddScoped<ICreateShiftUseCase, CreateShiftUseCase>();
            services.AddScoped<IAssignEmployeeService, AssignEmployeeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentNoteService, DepartmentNoteService>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDepartmentTaskService, DepartmentTaskService>();

            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IRelativeContactService, RelativeContactService>();
            // If there are more services, register them here.

            return services;
        }
    }
}
