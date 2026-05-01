using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Overlapssystem.Infrastructure.Repositories.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=KRISTINA;Database=OverlapssystemDB; Trusted_Connection=True; TrustServerCertificate=True;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}