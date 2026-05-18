using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Infrastructure.Identity;

namespace Overlapssystem.Api.Seed;

// Seeds mock employee users for development purposes only
public static class EmployeeSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();

        // Mock employees to seed in development
        var employees = new[]
        {
            new { Email = "anna@example.com", FirstName = "Anna", LastName = "Jensen" },
            new { Email = "bo@example.com", FirstName = "Bo", LastName = "Nielsen" },
            new { Email = "carl@example.com", FirstName = "Carl", LastName = "Hansen" },
            new { Email = "diana@example.com", FirstName = "Diana", LastName = "Pedersen" },
            new { Email = "eva@example.com", FirstName = "Eva", LastName = "Larsen" },
            new { Email = "finn@example.com", FirstName = "Finn", LastName = "Christensen" },
        };

        // Default password for all mock employees
        const string defaultPassword = "abc123";

        // Loop through each mock employee and create a user account if it doesn't already exist
        foreach (var emp in employees)
        {
            var existing = await userManager.FindByEmailAsync(emp.Email);
            if (existing != null)
                continue;

            var employeeId = await employeeService.CreateEmployeeAsync(
                emp.FirstName, emp.LastName);

            var user = new ApplicationUser
            {
                UserName = emp.Email,
                Email = emp.Email,
                EmployeeId = employeeId
            };

            var result = await userManager.CreateAsync(user, defaultPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, "Employee");
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create employee {emp.Email}: {errors}");
            }
        }
    }
}