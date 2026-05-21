using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.Services;

// Service responsible for handling traffic light updates for residents.
public class TrafficLightService : ITrafficLightService
{
    private readonly IAppDbContext _context;

    public TrafficLightService(IAppDbContext context)
    {
        _context = context;
    }

    // Updates the traffic light status for a specific resident schema.
    // and logs the action in the activity log. Returns true if the update was successful,
    // false if the traffic light value was invalid or the resident schema was not found.
    public async Task<bool> UpdateTrafficLightAsync(int residentSchemaId, string? trafficLight, int employeeId)
    {
        var schema = await _context.ResidentSchemas.FindAsync(residentSchemaId);
        if (schema == null) return false;

        if (trafficLight != null)
        {
            if (!Enum.TryParse<TrafficLight>(trafficLight, out var parsed))
                return false;
            schema.TrafficLight = parsed;
        }
        else
        {
            schema.TrafficLight = null;
        }

        _context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActionType = "Trafiklys",
            Description = $"Ændrede trafiklys til {ToDanish(schema.TrafficLight)}",
            Timestamp = DateTime.UtcNow,
            EmployeeId = employeeId,
            ResidentSchemaId = residentSchemaId
        });

        await _context.SaveChangesAsync();
        return true;
    }

    // Translates TrafficLight enum values to Danish for log descriptions. 
    private static string ToDanish(TrafficLight? value) => value switch
    {
        TrafficLight.Red => "Rød",
        TrafficLight.Yellow => "Gul",
        TrafficLight.Green => "Grøn",
        _ => "ingen"
    };
}
