using System;
using System.ComponentModel.DataAnnotations;
using Overlapssystem.Shared.Validation;

namespace Overlapssystem.Shared.DTOs;

//dto til at oprette en ny vagt
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