using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Enums;

// How a resident pays during shopping trips with staff
// Stored as int in the database
public enum PaymentMethod
{
    OwnCard = 0,
    PCard = 1, 
    Cash = 2

}
