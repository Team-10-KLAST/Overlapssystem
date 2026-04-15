using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Enums;

// Visual indicator of a resident's current state during a shift. 
// Stored as int in the database
public enum TrafficLight
{
    Green = 0, 
    Yellow = 1, 
    Red = 2
}
