using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Overlapssystem.Infrastructure.Identity;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;


namespace Overlapssystem.Infrastructure.Data
{
    // Inherits from IdentityDbContext so Identity tables (AspNetUsers, AspNetRoles, etc.) are included in the database
    // and share the same DbContext as the rest of the application data
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // DbContextOptions are provided via dependency injection (configured in Program.cs)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for EF Core so it can create tables and manage data for these entities
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Resident> Residents { get; set; }
        public DbSet<ResidentSchema> ResidentSchemas { get; set; }

        /// OnModelCreating is EF Core's hook for configuring the model. 
        /// We use it here to seed mock data so every developer
        /// gets the same residents and schemas in their local database. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Always call base.OnModelCreating first
            // - it lets EF Core apply its default conventions before we add our own
            base.OnModelCreating(modelBuilder);

            //Configure the one-to-one relationship between ApplicationUser and Employee
            modelBuilder.Entity<ApplicationUser>(user =>
            {
                user.HasOne(u => u.Employee)
                    .WithOne()
                    .HasForeignKey<ApplicationUser>(u => u.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //Configure Employee entity details
            modelBuilder.Entity<Employee>(employee =>
            {
                employee.HasKey(e => e.EmployeeId);
                employee.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                employee.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            });

            //Seed Residents - 8 mock residents 
            // Structure from excisting Exce-bases system.
            modelBuilder.Entity<Resident>().HasData(
                new Resident { ResidentId = 1, Initials = "L", ShoppingDay = "Tirsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.OwnCard },
                new Resident { ResidentId = 2, Initials = "T", ShoppingDay = "Onsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.PCard },
                new Resident { ResidentId = 3, Initials = "S", ShoppingDay = "Torsdag", MedicinTime = new TimeOnly(18, 0), PaymentMethod = PaymentMethod.OwnCard },
                new Resident { ResidentId = 4, Initials = "SH", ShoppingDay = "Mandag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.PCard },
                new Resident { ResidentId = 5, Initials = "A", ShoppingDay = "Fredag", MedicinTime = new TimeOnly(10, 0), PaymentMethod = PaymentMethod.Cash },
                new Resident { ResidentId = 6, Initials = "B", ShoppingDay = "Tirsdag", MedicinTime = new TimeOnly(7, 45), PaymentMethod = PaymentMethod.PCard },
                new Resident { ResidentId = 7, Initials = "J", ShoppingDay = "Onsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.Cash },
                new Resident { ResidentId = 8, Initials = "D", ShoppingDay = "Torsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.OwnCard }
            );

            //Seed ResidentSchemas - one per resident
            // Each schema is linked to its resident via ResidentId (FK) 
            modelBuilder.Entity<ResidentSchema>().HasData(
                new ResidentSchema { ResidentSchemaId = 1, ResidentId = 1, RelativeContact = "Mor: 12 34 56 78", Status = "Stabil dag, ingen bemærkninger.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 2, ResidentId = 2, RelativeContact = "Far: 23 45 67 89", Status = "Lidt urolig om morgenen, faldt til ro efter morgenmad.", TrafficLight = TrafficLight.Yellow },
                new ResidentSchema { ResidentSchemaId = 3, ResidentId = 3, RelativeContact = "Søster: 34 56 78 90", Status = "God dag, deltog i fællesaktiviteter.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 4, ResidentId = 4, RelativeContact = "Mor: 45 67 89 01", Status = "Skal have ekstra opsyn ved badning.", TrafficLight = TrafficLight.Yellow },
                new ResidentSchema { ResidentSchemaId = 5, ResidentId = 5, RelativeContact = "Bror: 56 78 90 12", Status = "Anspændt, har brug for ro.", TrafficLight = TrafficLight.Red },
                new ResidentSchema { ResidentSchemaId = 6, ResidentId = 6, RelativeContact = "Mor: 67 89 01 23", Status = "Glad og energisk hele dagen.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 7, ResidentId = 7, RelativeContact = "Far: 78 90 12 34", Status = "Lidt træt, sov middagslur.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 8, ResidentId = 8, RelativeContact = "Plejefar: 89 01 23 45", Status = "Konflikt med medbeboer, kræver opfølgning.", TrafficLight = TrafficLight.Red }
            );
        }
    }
}
