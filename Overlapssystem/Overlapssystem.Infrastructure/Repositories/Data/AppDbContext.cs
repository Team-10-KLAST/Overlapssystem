using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Overlapssystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Infrastructure.Repositories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //DbSet for Shift-entity, som repræsenterer vagter i databasen
        public DbSet<Shift> Shifts { get; set; } = null!;
    }
}

