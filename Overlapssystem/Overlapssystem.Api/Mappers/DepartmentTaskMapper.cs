using Overlapssystem.Shared.DTOs;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting DepartmentTask entities to DepartmentTaskDto objects.
public static class DepartmentTaskMapper
{
    // Maps a DepartmentTask entity to a DepartmentTaskDto, including the assigned employee's name.
    public static DepartmentTaskDto ToDto(DepartmentTask task, string assignedEmployee)
    {
        return new DepartmentTaskDto
        {
            Id = task.Id,
            TaskName = task.TaskName,
            AssignedEmployee = assignedEmployee
        };
    }
}
