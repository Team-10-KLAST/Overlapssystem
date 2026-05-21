using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Overlapssystem.Application.Services;

public class AssignEmployeeService : IAssignEmployeeService
{
    private readonly IAppDbContext _context;

    public AssignEmployeeService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position)
    {
        var schema = await _context.ResidentSchemas.FindAsync(residentSchemaId);
        if (schema == null) return false;

        // Hvis employeeId er 0, sætter vi feltet til null (fjerner tildeling)
        int? idToAssign = employeeId == 0 ? null : employeeId;

        if (position == 1) schema.PrimaryEmployeeId = idToAssign;
        else if (position == 2) schema.SecondaryEmployeeId = idToAssign;

        return await _context.SaveChangesAsync() > 0;
    }
}
