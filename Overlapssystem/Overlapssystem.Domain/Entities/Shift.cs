namespace Overlapssystem.Domain.Entities;

public class Shift
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public Overlapssystem.Shared.ShiftType Type { get; set; }

    public string EmployeeId { get; set; } = string.Empty;
}