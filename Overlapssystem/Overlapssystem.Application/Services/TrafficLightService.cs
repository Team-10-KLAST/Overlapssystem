using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.Services
{
    // Service responsible for handling traffic light updates for residents.
    public class TrafficLightService : ITrafficLightService
    {
        private readonly IAppDbContext _context;

        public TrafficLightService(IAppDbContext context)
        {
            _context = context;
        }

        // Updates the traffic light status for a specific resident schema.
        // and logs the action in the activity log
        public async Task UpdateTrafficLightAsync(int residentSchemaId, string? trafficLight, int employeeId)
        {
            TrafficLight? parsedTrafficLight = null;

            if (trafficLight != null)
            {
                if (!Enum.TryParse<TrafficLight>(trafficLight, out var value))
                    throw new ArgumentException("Ugyldigt trafiklys-niveau");
                parsedTrafficLight = value;
            }

            var schema = await _context.ResidentSchemas.FindAsync(residentSchemaId);
            if (schema == null) throw new KeyNotFoundException();

            schema.TrafficLight = parsedTrafficLight;

            _context.ActivityLogEntries.Add(new ActivityLogEntry
            {
                ActionType = "Trafiklys",
                Description = $"Ændrede trafiklys til {ToDanish(parsedTrafficLight)}",
                Timestamp = DateTime.UtcNow,
                EmployeeId = employeeId,
                ResidentSchemaId = residentSchemaId
            });

            await _context.SaveChangesAsync();
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
}
