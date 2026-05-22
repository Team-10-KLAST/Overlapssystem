using Microsoft.EntityFrameworkCore;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// This interface abstracts the database context for the application,
public interface IAppDbContext
{
    DbSet<Employee> Employees { get; }
    DbSet<Phone> Phones { get; }
    DbSet<Resident> Residents { get; }
    DbSet<ResidentSchema> ResidentSchemas { get; }
    DbSet<Department> Departments { get; }
    DbSet<Shift> Shifts { get; }
    DbSet<MedicationTime> MedicationTimes { get; }
    DbSet<MedicationRegistration> MedicationRegistrations { get; }
    DbSet<PN> PNs { get; }
    DbSet<DepartmentNote> DepartmentNotes { get; }
    DbSet<DepartmentTask> DepartmentTasks { get; }
    DbSet<ShiftTaskAssignment> ShiftTaskAssignments { get; }

    DbSet<ActivityLogEntry> ActivityLogEntries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
