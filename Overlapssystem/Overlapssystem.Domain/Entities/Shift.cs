using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

//Shift-entity som repræsenterer en vagt i systemet
public class Shift
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ShiftType Type { get; set; }
    public int DepartmentId { get; set; }
}