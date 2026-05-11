using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Domain.Interfaces;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    int? ModifiedByEmployeeId { get; set; }
    Employee? ModifiedByEmployee { get; set; }
}