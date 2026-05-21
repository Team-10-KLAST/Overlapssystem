using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overlapssystem.Infrastructure.Identity;
using Overlapssystem.Application.Interfaces;

namespace Overlapssystem.Api.Seed;

public static class IdentitySeeder
{
    // This method seeds the database with initial roles and an admin user.
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        // Get required services
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        // Ensure roles exist
        var roles = new[] { "Admin", "Employee" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Fallback values for admin credentials if not set in configuration
        var adminEmail = configuration["Seed:AdminEmail"] ?? "admin@example.com";
        var adminPassword = configuration["Seed:AdminPassword"] ?? "abc123";

        // Create admin user if it doesn't exist
        var existing = await userManager.FindByEmailAsync(adminEmail);
        if (existing == null)
        {
            var employeeId = await employeeService.CreateEmployeeAsync("Admin", "User");

            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmployeeId = employeeId
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create admin user: {errors}");
            }
        }
    }
}
