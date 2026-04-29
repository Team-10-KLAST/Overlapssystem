using System;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Shared.DTOs;

//dto til at oprette en ny vagt
public class CreateShiftDto
{
    public DateTime Date { get; set; }
    public ShiftType ShiftType { get; set; }
    public DepartmentType Department { get; set; }
}