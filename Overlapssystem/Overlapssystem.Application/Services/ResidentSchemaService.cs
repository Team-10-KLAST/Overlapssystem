using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// concrete implementation of IResidentSchemaService 
// Handles the business logic of fetching resident schemas. 
public class ResidentSchemaService : IResidentSchemaService
{
    
    private readonly IAppDbContext _context;

    public ResidentSchemaService(IAppDbContext context)
    {
        _context = context;
    }


    // Fetches all resident schemas from the database. 
    // NOTE: Currently returns ALL schemas (mock data) 
    // When Shift feature is ready, add filtering by department/shift. 
    public async Task<IEnumerable<ResidentSchema>> GetAllAsync()
    {
        return await _context.ResidentSchemas
            .Include(s => s.Resident)
            .ToListAsync();
    }
}
