using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Models;

// This record represents the data required to generate a JWT token for an authenticated user.
public record TokenRequest(
    string UserId,
    string Email,
    string Role,
    string EmployeeId);


