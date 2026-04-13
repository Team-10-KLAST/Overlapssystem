using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Overlapssystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        // DbSets tilføjes efterhånden som du opretter entities
        // Eksempel (tilføj selv når du har oprettet klassen):
        // public DbSet<Beboer> Beboere { get; set; }
    }
}
