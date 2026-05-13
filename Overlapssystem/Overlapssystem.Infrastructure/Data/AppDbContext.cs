using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Domain.Interfaces;
using Overlapssystem.Infrastructure.Identity;


namespace Overlapssystem.Infrastructure.Data;

// Inherits from IdentityDbContext so Identity tables (AspNetUsers, AspNetRoles, etc.) are included in the database
// and share the same DbContext as the rest of the application data
public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
{
    // DbContextOptions are provided via dependency injection (configured in Program.cs)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSets for EF Core so it can create tables and manage data for these entities
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Resident> Residents => Set<Resident>();
    public DbSet<ResidentSchema> ResidentSchemas => Set<ResidentSchema>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<MedicationTime> MedicationTimes => Set<MedicationTime>();
    public DbSet<MedicationRegistration> MedicationRegistrations => Set<MedicationRegistration>();
    public DbSet<PN> PNs => Set<PN>();
    public DbSet<DepartmentNote> DepartmentNotes => Set<DepartmentNote>();


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


        modelBuilder.Entity<PN>()
            .HasOne(p => p.ResidentSchema)
            .WithMany()
            .HasForeignKey(p => p.ResidentSchemaId)
            .OnDelete(DeleteBehavior.NoAction);



        //Configure the relationships for Resident, ResidentSchema,
        //MedicationTime, and MedicationRegistration
        modelBuilder.Entity<MedicationRegistration>()
            .HasOne(mr => mr.ResidentSchema)
            .WithMany(rs => rs.MedicationRegistrations)
            .HasForeignKey(mr => mr.ResidentSchemaId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<MedicationRegistration>()
            .HasOne(mr => mr.MedicationTime)
            .WithMany()
            .HasForeignKey(mr => mr.MedicationTimeId)
            .OnDelete(DeleteBehavior.NoAction);


        // Configure Employee relationships on ResidentSchema
        // Three separate FK's to Employee — must be explicit to avoid EF Core confusion
        modelBuilder.Entity<ResidentSchema>()
            .HasOne(rs => rs.PrimaryEmployee)
            .WithMany()
            .HasForeignKey(rs => rs.PrimaryEmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ResidentSchema>()
            .HasOne(rs => rs.SecondaryEmployee)
            .WithMany()
            .HasForeignKey(rs => rs.SecondaryEmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ResidentSchema>()
            .HasOne(rs => rs.ModifiedByEmployee)
            .WithMany()
            .HasForeignKey(rs => rs.ModifiedByEmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Employee relationship on MedicationRegistration
        modelBuilder.Entity<MedicationRegistration>()
            .HasOne(mr => mr.ModifiedByEmployee)
            .WithMany()
            .HasForeignKey(mr => mr.ModifiedByEmployeeId)
            .OnDelete(DeleteBehavior.NoAction);


        //Seed Residents - 8 mock residents 
        // Structure from excisting Exce-bases system.
        // Seed departments 
        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "Skoven" },
            new Department { DepartmentId = 2, DepartmentName = "Slottet" }
            );

        // Seed a default shift for mock data purposes.
        modelBuilder.Entity<Shift>().HasData(
            new Shift { Id = 1, Date = new DateTime(2026, 4, 28), Type = ShiftType.Dagvagt, DepartmentId = 1 },
            new Shift { Id = 2, Date = new DateTime(2026, 4, 28), Type = ShiftType.Dagvagt, DepartmentId = 2 }
        );


        // Seed Residents - 16 mock residents, 8 per department.
        // Skoven (DepartmentId = 1): L, T, S, SH, A, B, J, D
        // Slottet (DepartmentId = 2): M, K, P, R, N, F, E, H
        modelBuilder.Entity<Resident>().HasData(
           // Skoven
           new Resident { ResidentId = 1, Initials = "L", ShoppingDay = "Tirsdag", PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 1 },
           new Resident { ResidentId = 2, Initials = "T", ShoppingDay = "Onsdag", PaymentMethod = PaymentMethod.PCard, DepartmentId = 1 },
           new Resident { ResidentId = 3, Initials = "S", ShoppingDay = "Torsdag", PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 1 },
           new Resident { ResidentId = 4, Initials = "SH", ShoppingDay = "Mandag", PaymentMethod = PaymentMethod.PCard, DepartmentId = 1 },
           new Resident { ResidentId = 5, Initials = "A", ShoppingDay = "Fredag", PaymentMethod = PaymentMethod.Cash, DepartmentId = 1 },
           new Resident { ResidentId = 6, Initials = "B", ShoppingDay = "Tirsdag", PaymentMethod = PaymentMethod.PCard, DepartmentId = 1 },
           new Resident { ResidentId = 7, Initials = "J", ShoppingDay = "Onsdag", PaymentMethod = PaymentMethod.Cash, DepartmentId = 1 },
           new Resident { ResidentId = 8, Initials = "D", ShoppingDay = "Torsdag", PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 1 },
           // Slottet
           new Resident { ResidentId = 9, Initials = "M", ShoppingDay = "Mandag", PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 2 },
           new Resident { ResidentId = 10, Initials = "K", ShoppingDay = "Tirsdag", PaymentMethod = PaymentMethod.PCard, DepartmentId = 2 },
           new Resident { ResidentId = 11, Initials = "P", ShoppingDay = "Onsdag", PaymentMethod = PaymentMethod.Cash, DepartmentId = 2 },
           new Resident { ResidentId = 12, Initials = "R", ShoppingDay = "Torsdag", PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 2 },
           new Resident { ResidentId = 13, Initials = "N", ShoppingDay = "Fredag", PaymentMethod = PaymentMethod.PCard, DepartmentId = 2 },
           new Resident { ResidentId = 14, Initials = "F", ShoppingDay = "Mandag", PaymentMethod = PaymentMethod.Cash, DepartmentId = 2 },
           new Resident { ResidentId = 15, Initials = "E", ShoppingDay = "Onsdag", PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 2 },
           new Resident { ResidentId = 16, Initials = "H", ShoppingDay = "Fredag", PaymentMethod = PaymentMethod.PCard, DepartmentId = 2 }
       );

        // Seed MedicationTimes - each resident gets one or two planned medication times.
        modelBuilder.Entity<MedicationTime>().HasData(
            // Skoven
            new MedicationTime { MedicationTimeId = 1, ResidentId = 1, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 2, ResidentId = 2, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 3, ResidentId = 3, PlannedTime = new TimeOnly(18, 0) },
            new MedicationTime { MedicationTimeId = 4, ResidentId = 4, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 5, ResidentId = 5, PlannedTime = new TimeOnly(10, 0) },
            new MedicationTime { MedicationTimeId = 6, ResidentId = 6, PlannedTime = new TimeOnly(7, 45) },
            new MedicationTime { MedicationTimeId = 7, ResidentId = 7, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 8, ResidentId = 8, PlannedTime = new TimeOnly(8, 0) },
            // Slottet
            new MedicationTime { MedicationTimeId = 9, ResidentId = 9, PlannedTime = new TimeOnly(9, 0) },
            new MedicationTime { MedicationTimeId = 10, ResidentId = 10, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 11, ResidentId = 11, PlannedTime = new TimeOnly(12, 0) },
            new MedicationTime { MedicationTimeId = 12, ResidentId = 12, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 13, ResidentId = 13, PlannedTime = new TimeOnly(17, 0) },
            new MedicationTime { MedicationTimeId = 14, ResidentId = 14, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 15, ResidentId = 15, PlannedTime = new TimeOnly(20, 0) },
            new MedicationTime { MedicationTimeId = 16, ResidentId = 16, PlannedTime = new TimeOnly(8, 0) },
            new MedicationTime { MedicationTimeId = 17, ResidentId = 1, PlannedTime = new TimeOnly(20, 0) }
        );

        // Seed ResidentSchemas - one per resident (16 total).
        // RelativeContact contains daily notes about family interactions.
        modelBuilder.Entity<ResidentSchema>().HasData(
            // Skoven
            new ResidentSchema { ResidentSchemaId = 1, ResidentId = 1, ShiftId = 1, RelativeContact = "Mor ringede, kommer på besøg torsdag eftermiddag.", Status = "Stabil dag, ingen bemærkninger.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 2, ResidentId = 2, ShiftId = 1, RelativeContact = "Ingen kontakt i dag.", Status = "Lidt urolig om morgenen, faldt til ro efter morgenmad.", TrafficLight = TrafficLight.Yellow },
            new ResidentSchema { ResidentSchemaId = 3, ResidentId = 3, ShiftId = 1, RelativeContact = "Søster hentede til frokost, aftale om næste besøg fredag.", Status = "God dag, deltog i fællesaktiviteter.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 4, ResidentId = 4, ShiftId = 1, RelativeContact = "Far besøgte kl. 14, gik godt.", Status = "Skal have ekstra opsyn ved badning.", TrafficLight = TrafficLight.Yellow },
            new ResidentSchema { ResidentSchemaId = 5, ResidentId = 5, ShiftId = 1, RelativeContact = "Mor ringede, beboer ville ikke tale i telefon.", Status = "Anspændt, har brug for ro.", TrafficLight = TrafficLight.Red },
            new ResidentSchema { ResidentSchemaId = 6, ResidentId = 6, ShiftId = 1, RelativeContact = "Ingen kontakt i dag.", Status = "Glad og energisk hele dagen.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 7, ResidentId = 7, ShiftId = 1, RelativeContact = "Plejefar afleverede tøj, kort besøg.", Status = "Lidt træt, sov middagslur.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 8, ResidentId = 8, ShiftId = 1, RelativeContact = "Aftalt med mor at hun ringer i morgen kl. 10.", Status = "Konflikt med medbeboer, kræver opfølgning.", TrafficLight = TrafficLight.Red },
            // Slottet
            new ResidentSchema { ResidentSchemaId = 9, ResidentId = 9, ShiftId = 2, RelativeContact = "Mor kom forbi med kage, hyggeligt besøg.", Status = "Rolig dag, god stemning.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 10, ResidentId = 10, ShiftId = 2, RelativeContact = "Ingen kontakt i dag.", Status = "Nægtede morgenmad, spiste lidt til frokost.", TrafficLight = TrafficLight.Yellow },
            new ResidentSchema { ResidentSchemaId = 11, ResidentId = 11, ShiftId = 2, RelativeContact = "Bror ringede, vil besøge i weekenden.", Status = "Deltog i musik-aktivitet, sang med.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 12, ResidentId = 12, ShiftId = 2, RelativeContact = "Plejemor hentede til tandlæge kl. 10.", Status = "Træt efter tandlægebesøg, hvilede sig.", TrafficLight = TrafficLight.Yellow },
            new ResidentSchema { ResidentSchemaId = 13, ResidentId = 13, ShiftId = 2, RelativeContact = "Far ringede, beboer glad for samtalen.", Status = "God dag, hjalp med at dække bord.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 14, ResidentId = 14, ShiftId = 2, RelativeContact = "Ingen kontakt i dag.", Status = "Urolig, havde svært ved at falde til ro.", TrafficLight = TrafficLight.Red },
            new ResidentSchema { ResidentSchemaId = 15, ResidentId = 15, ShiftId = 2, RelativeContact = "Mor og søster besøgte, gik en tur sammen.", Status = "Glad efter besøg, snakkede om det hele aftenen.", TrafficLight = TrafficLight.Green },
            new ResidentSchema { ResidentSchemaId = 16, ResidentId = 16, ShiftId = 2, RelativeContact = "Aftalt med far at han kommer lørdag.", Status = "Stille dag, så film om eftermiddagen.", TrafficLight = TrafficLight.Green }
        );
    }

    // Automatically sets CreatedAt and UpdatedAt on entities implementing IAuditable
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;

            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
