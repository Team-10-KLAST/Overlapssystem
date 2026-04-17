using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Application.Services;

// concrete implementation of IResidentSchemaService 
// Handles the business logic of fetching resident schemas. 
// The DbConterxt is injected via the constructor. 
public class ResidentSchemaService : IResidentSchemaService
{
    
    private readonly AppDbContext _context;

    public ResidentSchemaService( AppDbContext context)
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
