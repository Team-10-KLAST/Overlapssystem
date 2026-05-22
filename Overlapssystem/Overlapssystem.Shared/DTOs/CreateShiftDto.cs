using System;
using System.ComponentModel.DataAnnotations;
using Overlapssystem.Shared.Validation;

namespace Overlapssystem.Shared.DTOs;

// DTO for creating a new shift.
// Includes validation to ensure date, shift type and department are valid.
public class CreateShiftDto
{
    [Required]
    [DataType(DataType.Date)]
    [DateNotInFuture]
    public DateTime Date { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Husk at vælge vagttype.")]
    public int ShiftType { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Husk at vælge afdeling.")]
    public int DepartmentId { get; set; }
}