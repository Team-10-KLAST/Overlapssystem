using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs
{
    // DTO used when the client wants to update a resident's traffic light status.
    public class UpdateTrafficLightDto
    {
        public string? TrafficLight { get; set; }
    }
}
