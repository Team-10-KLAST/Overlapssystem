using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

// Represent a resident (borger) with its "master data" 
// A resident can have many ResidentSchemas over time (one per shift). 
public class Resident
{
    // Primary key.
    public int ResidentId { get; set; }
    public string Initials { get; set; } = string.Empty;
    // Which days of the week the resident typically goes shopping
    // Free text because the format can vary ("Tirsdag", "Fredag kl 15")
    public string ShoppingDay { get; set; } = string.Empty;
    // The time of day medication is given 
    public TimeOnly MedicinTime { get; set; }
    //How the resident pays when shopping. Enum -> stored in database as int. 
    public PaymentMethod PaymentMethod { get; set; }

    // Foreign key to the department this resident lives in.
    // Navigation is uni-directional: Resident -> Department only. 
    public int DepartmentId { get; set; }
}
