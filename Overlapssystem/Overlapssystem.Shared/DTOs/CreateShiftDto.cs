using System;

namespace Overlapssystem.Shared.DTOs;

//dto til at oprette en ny vagt
public class CreateShiftDto
{
    public DateTime Date { get; set; }
    public int ShiftType { get; set; }
    public int DepartmentId { get; set; }
}