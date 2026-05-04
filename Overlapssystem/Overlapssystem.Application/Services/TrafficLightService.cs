using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;
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
        public async Task UpdateTrafficLightAsync(int residentSchemaId, string? trafficLight)
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
            await _context.SaveChangesAsync();
        }
    }
}
