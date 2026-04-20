using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Overlapssystem.Domain.Entities;


namespace Overlapssystem.Infrastructure.Identity
{
    // ApplicationUser extends IdentityUser to include a reference to an Employee
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
