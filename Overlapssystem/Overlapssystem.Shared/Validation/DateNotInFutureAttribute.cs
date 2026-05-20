using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.Validation;

public class DateNotInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is DateTime date && date.Date > DateTime.Today)
            return new ValidationResult("Dato må ikke være i fremtiden.");
        return ValidationResult.Success;
    }
}

