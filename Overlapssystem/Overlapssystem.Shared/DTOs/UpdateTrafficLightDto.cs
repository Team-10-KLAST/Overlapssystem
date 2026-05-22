using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO for updating a resident's traffic light status.
// Accepts null to allow clearing the traffic light value.
public class UpdateTrafficLightDto
{
    [RegularExpression(@"^(Red|Yellow|Green)$",
        ErrorMessage = "TrafficLight must be Red, Yellow or Green.")]
    public string? TrafficLight { get; set; }
}
