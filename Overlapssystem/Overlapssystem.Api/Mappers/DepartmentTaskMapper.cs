using Overlapssystem.Shared.DTOs;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Api.Mappers
{
    public static class DepartmentTaskMapper
    {
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
}
