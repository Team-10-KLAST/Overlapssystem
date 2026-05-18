using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Models
{
    // This record represents the result of a user registration attempt, including whether it was successful
    // and any errors that occurred during the process.
    public record RegisterResult(
        bool Success,
        IEnumerable<string> Errors);
}
