using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO used when the client wants to update a resident's traffic light status.
public class UpdateTrafficLightDto
{
    [RegularExpression(@"^(Red|Yellow|Green)$",
        ErrorMessage = "TrafficLight must be Red, Yellow or Green.")]
    public string? TrafficLight { get; set; }
}
