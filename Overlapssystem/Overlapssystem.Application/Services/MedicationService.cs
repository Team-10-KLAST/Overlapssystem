using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services
{
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
        public async Task MarkAsGivenAsync(int residentSchemaId, int medicationTimeId)
        {
            var alreadyRegistered = await _context.MedicationRegistrations
                .AnyAsync(mr => mr.ResidentSchemaId == residentSchemaId
                    && mr.MedicationTimeId == medicationTimeId
                    && mr.Given);

            if (alreadyRegistered)
                return;

            var registration = new MedicationRegistration
            {
                ResidentSchemaId = residentSchemaId,
                MedicationTimeId = medicationTimeId,
                Given = true,
                GivenTime = DateTime.UtcNow
            };

            _context.MedicationRegistrations.Add(registration);
            await _context.SaveChangesAsync();
        }
    }
}
