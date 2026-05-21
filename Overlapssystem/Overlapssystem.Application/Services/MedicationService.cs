using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Handles business logic for medication administration.
// Responsible for marking planned medication times as given in a specific shift.
public class MedicationService : IMedicationService
{
    private readonly IAppDbContext _context;

    public MedicationService(IAppDbContext context)
    {
        _context = context;
    }

    // Creates a new MedicationRegistration when a staff member marks a medication as given.
    // and logs the action in the activity log
    public async Task MarkAsGivenAsync(int residentSchemaId, int medicationTimeId, int employeeId)
    {
        var alreadyRegistered = await _context.MedicationRegistrations
            .AnyAsync(mr => mr.ResidentSchemaId == residentSchemaId
                && mr.MedicationTimeId == medicationTimeId
                && mr.Given);

        if (alreadyRegistered)
            return;

        var medicationTime = await _context.MedicationTimes.FindAsync(medicationTimeId);

        var registration = new MedicationRegistration
        {
            ResidentSchemaId = residentSchemaId,
            MedicationTimeId = medicationTimeId,
            Given = true,
            GivenTime = DateTime.UtcNow
        };

        _context.MedicationRegistrations.Add(registration);

        _context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActionType = "Medicin",
            Description = $"Markerede medicin som givet kl. {medicationTime?.PlannedTime.ToString("HH:mm") ?? "ukendt"}",
            Timestamp = DateTime.UtcNow,
            EmployeeId = employeeId,
            ResidentSchemaId = residentSchemaId
        });
        await _context.SaveChangesAsync();
    }
}
