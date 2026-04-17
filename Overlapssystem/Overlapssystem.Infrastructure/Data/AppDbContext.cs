using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;

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

        public DbSet<Resident> Residents { get; set; }
        public DbSet<ResidentSchema> ResidentSchemas { get; set; }
        public DbSet<Department> Departments { get; set; }



        /// OnModelCreating is EF Core's hook for configuring the model. 
        /// We use it here to seed mock data so every developer
        /// gets the same residents and schemas in their local database. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Always call base.OnModelCreating first
            // - it lets EF Core apply its default conventions before we add our own
            base.OnModelCreating(modelBuilder);

            // Seed departments 
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "Skoven" },
                new Department {  DepartmentId = 2, DepartmentName = "Slottet"}
                );


            // Seed Residents - 16 mock residents, 8 per department.
            // Skoven (DepartmentId = 1): L, T, S, SH, A, B, J, D
            // Slottet (DepartmentId = 2): M, K, P, R, N, F, E, H
            modelBuilder.Entity<Resident>().HasData(
                // Skoven
                new Resident { ResidentId = 1, Initials = "L", ShoppingDay = "Tirsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 1 },
                new Resident { ResidentId = 2, Initials = "T", ShoppingDay = "Onsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.PCard, DepartmentId = 1 },
                new Resident { ResidentId = 3, Initials = "S", ShoppingDay = "Torsdag", MedicinTime = new TimeOnly(18, 0), PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 1 },
                new Resident { ResidentId = 4, Initials = "SH", ShoppingDay = "Mandag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.PCard, DepartmentId = 1 },
                new Resident { ResidentId = 5, Initials = "A", ShoppingDay = "Fredag", MedicinTime = new TimeOnly(10, 0), PaymentMethod = PaymentMethod.Cash, DepartmentId = 1 },
                new Resident { ResidentId = 6, Initials = "B", ShoppingDay = "Tirsdag", MedicinTime = new TimeOnly(7, 45), PaymentMethod = PaymentMethod.PCard, DepartmentId = 1 },
                new Resident { ResidentId = 7, Initials = "J", ShoppingDay = "Onsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.Cash, DepartmentId = 1 },
                new Resident { ResidentId = 8, Initials = "D", ShoppingDay = "Torsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 1 },
                // Slottet
                new Resident { ResidentId = 9, Initials = "M", ShoppingDay = "Mandag", MedicinTime = new TimeOnly(9, 0), PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 2 },
                new Resident { ResidentId = 10, Initials = "K", ShoppingDay = "Tirsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.PCard, DepartmentId = 2 },
                new Resident { ResidentId = 11, Initials = "P", ShoppingDay = "Onsdag", MedicinTime = new TimeOnly(12, 0), PaymentMethod = PaymentMethod.Cash, DepartmentId = 2 },
                new Resident { ResidentId = 12, Initials = "R", ShoppingDay = "Torsdag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 2 },
                new Resident { ResidentId = 13, Initials = "N", ShoppingDay = "Fredag", MedicinTime = new TimeOnly(17, 0), PaymentMethod = PaymentMethod.PCard, DepartmentId = 2 },
                new Resident { ResidentId = 14, Initials = "F", ShoppingDay = "Mandag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.Cash, DepartmentId = 2 },
                new Resident { ResidentId = 15, Initials = "E", ShoppingDay = "Onsdag", MedicinTime = new TimeOnly(20, 0), PaymentMethod = PaymentMethod.OwnCard, DepartmentId = 2 },
                new Resident { ResidentId = 16, Initials = "H", ShoppingDay = "Fredag", MedicinTime = new TimeOnly(8, 0), PaymentMethod = PaymentMethod.PCard, DepartmentId = 2 }
            );

            // Seed ResidentSchemas - one per resident (16 total).
            // RelativeContact contains daily notes about family interactions.
            modelBuilder.Entity<ResidentSchema>().HasData(
                // Skoven
                new ResidentSchema { ResidentSchemaId = 1, ResidentId = 1, RelativeContact = "Mor ringede, kommer på besøg torsdag eftermiddag.", Status = "Stabil dag, ingen bemærkninger.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 2, ResidentId = 2, RelativeContact = "Ingen kontakt i dag.", Status = "Lidt urolig om morgenen, faldt til ro efter morgenmad.", TrafficLight = TrafficLight.Yellow },
                new ResidentSchema { ResidentSchemaId = 3, ResidentId = 3, RelativeContact = "Søster hentede til frokost, aftale om næste besøg fredag.", Status = "God dag, deltog i fællesaktiviteter.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 4, ResidentId = 4, RelativeContact = "Far besøgte kl. 14, gik godt.", Status = "Skal have ekstra opsyn ved badning.", TrafficLight = TrafficLight.Yellow },
                new ResidentSchema { ResidentSchemaId = 5, ResidentId = 5, RelativeContact = "Mor ringede, beboer ville ikke tale i telefon.", Status = "Anspændt, har brug for ro.", TrafficLight = TrafficLight.Red },
                new ResidentSchema { ResidentSchemaId = 6, ResidentId = 6, RelativeContact = "Ingen kontakt i dag.", Status = "Glad og energisk hele dagen.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 7, ResidentId = 7, RelativeContact = "Plejefar afleverede tøj, kort besøg.", Status = "Lidt træt, sov middagslur.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 8, ResidentId = 8, RelativeContact = "Aftalt med mor at hun ringer i morgen kl. 10.", Status = "Konflikt med medbeboer, kræver opfølgning.", TrafficLight = TrafficLight.Red },
                // Slottet
                new ResidentSchema { ResidentSchemaId = 9, ResidentId = 9, RelativeContact = "Mor kom forbi med kage, hyggeligt besøg.", Status = "Rolig dag, god stemning.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 10, ResidentId = 10, RelativeContact = "Ingen kontakt i dag.", Status = "Nægtede morgenmad, spiste lidt til frokost.", TrafficLight = TrafficLight.Yellow },
                new ResidentSchema { ResidentSchemaId = 11, ResidentId = 11, RelativeContact = "Bror ringede, vil besøge i weekenden.", Status = "Deltog i musik-aktivitet, sang med.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 12, ResidentId = 12, RelativeContact = "Plejemor hentede til tandlæge kl. 10.", Status = "Træt efter tandlægebesøg, hvilede sig.", TrafficLight = TrafficLight.Yellow },
                new ResidentSchema { ResidentSchemaId = 13, ResidentId = 13, RelativeContact = "Far ringede, beboer glad for samtalen.", Status = "God dag, hjalp med at dække bord.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 14, ResidentId = 14, RelativeContact = "Ingen kontakt i dag.", Status = "Urolig, havde svært ved at falde til ro.", TrafficLight = TrafficLight.Red },
                new ResidentSchema { ResidentSchemaId = 15, ResidentId = 15, RelativeContact = "Mor og søster besøgte, gik en tur sammen.", Status = "Glad efter besøg, snakkede om det hele aftenen.", TrafficLight = TrafficLight.Green },
                new ResidentSchema { ResidentSchemaId = 16, ResidentId = 16, RelativeContact = "Aftalt med far at han kommer lørdag.", Status = "Stille dag, så film om eftermiddagen.", TrafficLight = TrafficLight.Green }
            );

        }
    }
    
}
