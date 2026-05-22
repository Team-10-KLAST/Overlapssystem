using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Overlapssystem.Domain.Entities;


namespace Overlapssystem.Infrastructure.Identity;

// Extends the default IdentityUser with a reference to an Employee entity.
// This links an authenticated user account to an employee record in the system.
public class ApplicationUser : IdentityUser
{
    public int? EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
}
