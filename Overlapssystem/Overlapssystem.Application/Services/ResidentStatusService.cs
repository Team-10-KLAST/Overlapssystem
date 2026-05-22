using Overlapssystem.Application.Interfaces;

namespace Overlapssystem.Application.Services;

// Handles updating the status text on resident schemas. 
public class ResidentStatusService : IResidentStatusService
{
    private readonly IAppDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public ResidentStatusService(IAppDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
            
    }

    // Updates status text for a specifik resident schema and logs the action.
    // Returns false if not found. 
    public async Task<bool> UpdateStatusAsync(int residentSchemaId, string newStatus, int employeeId)
    {
        var schema = await _context.ResidentSchemas.FindAsync(residentSchemaId);

        if (schema == null)
        {
            return false;
        }

        schema.Status = newStatus;

        await _activityLogService.AddLogEntry(
            "Status",
            $"Tilføjede til status: {newStatus}",
            employeeId,
            residentSchemaId);

        await _context.SaveChangesAsync();

        return true;
    }
}
