using Microsoft.Extensions.DependencyInjection;
using Overlapssystem.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Overlapssystem.Api.Seed;

// Seeds default phone numbers for development purposes only
public static class PhoneSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var phoneService = scope.ServiceProvider.GetRequiredService<IPhoneService>();

        // The specific phone numbers required to seed
        var phoneNumbers = new[] { "41522", "41523", "41524", "41525", "41526", "41527", "41529" };

        // Retrieve existing phones from the database to avoid duplicate entries
        var existingPhones = await phoneService.GetAllPhonesAsync();
        var existingNumbers = existingPhones.Select(p => p.Number).ToHashSet();

        // Loop through each phone number and create it if it doesn't already exist
        foreach (var number in phoneNumbers)
        {
            if (existingNumbers.Contains(number))
                continue;

            try
            {                
                await phoneService.CreatePhoneAsync(number);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to seed phone number {number}: {ex.Message}");
            }
        }
    }
}