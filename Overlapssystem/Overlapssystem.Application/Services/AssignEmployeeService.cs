using Overlapssystem.Application.Interfaces;

namespace Overlapssystem.Application.Services;

// Handles assigning employees to resident chemas. 
public class AssignEmployeeService : IAssignEmployeeService
{
    private readonly IAppDbContext _context;

    public AssignEmployeeService(IAppDbContext context)
    {
        _context = context;
    }

    // Assigns an employee to a resident schema at a given position. 
    public async Task<bool> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position)
    {
        var schema = await _context.ResidentSchemas.FindAsync(residentSchemaId);
        if (schema == null) return false;

        int? idToAssign = employeeId == 0 ? null : employeeId;

        if (position == 1) schema.PrimaryEmployeeId = idToAssign;
        else if (position == 2) schema.SecondaryEmployeeId = idToAssign;

        return await _context.SaveChangesAsync() > 0;
    }
}
