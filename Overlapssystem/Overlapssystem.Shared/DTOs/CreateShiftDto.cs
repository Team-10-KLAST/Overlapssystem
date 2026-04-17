using System;

namespace Overlapssystem.Shared.DTOs;

public class CreateShiftDto
{
    public DateTime Date { get; set; }
    public ShiftType ShiftType { get; set; }
}