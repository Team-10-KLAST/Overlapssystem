namespace Overlapssystem.Domain.Entities;

//Shift-entity som repræsenterer en vagt i systemet
public class Shift
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Overlapssystem.Domain.Enums.ShiftType Type { get; set; }
    public Overlapssystem.Domain.Enums.DepartmentType Department { get; set; }
}