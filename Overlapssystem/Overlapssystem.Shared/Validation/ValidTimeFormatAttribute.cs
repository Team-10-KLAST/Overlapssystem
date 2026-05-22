using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.Validation;

// Custom validation attribute that ensures a date value is not set in the future.
public class ValidTimeFormatAttribute : ValidationAttribute
{
    // Returns a validation error if the provided date is later than today.
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is string time && !TimeOnly.TryParse(time, out _))
            return new ValidationResult("Tidsformat skal være HH:mm");
        
        return ValidationResult.Success;
    }
}

