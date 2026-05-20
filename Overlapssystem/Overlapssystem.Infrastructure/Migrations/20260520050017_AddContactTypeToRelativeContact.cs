using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContactTypeToRelativeContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // RelativeContacts table already exists in the database.
            // Only add the new ContactType column.
            migrationBuilder.AddColumn<string>(
                name: "ContactType",
                table: "RelativeContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Note");

            // Add index on RelativeContacts(ResidentSchemaId) only if it does not exist.
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = 'IX_RelativeContacts_ResidentSchemaId'
                      AND object_id = OBJECT_ID('RelativeContacts'))
                BEGIN
                    CREATE INDEX [IX_RelativeContacts_ResidentSchemaId]
                        ON [RelativeContacts] ([ResidentSchemaId]);
                END");

            // Add FK from RelativeContacts to ResidentSchemas only if it does not exist.
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.foreign_keys
                    WHERE name = 'FK_RelativeContacts_ResidentSchemas_ResidentSchemaId')
                BEGIN
                    ALTER TABLE [RelativeContacts]
                        ADD CONSTRAINT [FK_RelativeContacts_ResidentSchemas_ResidentSchemaId]
                        FOREIGN KEY ([ResidentSchemaId])
                        REFERENCES [ResidentSchemas] ([ResidentSchemaId])
                        ON DELETE CASCADE;
                END");

            // Add index on Residents(DepartmentId) only if it does not exist.
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = 'IX_Residents_DepartmentId'
                      AND object_id = OBJECT_ID('Residents'))
                BEGIN
                    CREATE INDEX [IX_Residents_DepartmentId]
                        ON [Residents] ([DepartmentId]);
                END");

            // Add FK from Residents to Departments only if it does not exist.
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.foreign_keys
                    WHERE name = 'FK_Residents_Departments_DepartmentId')
                BEGIN
                    ALTER TABLE [Residents]
                        ADD CONSTRAINT [FK_Residents_Departments_DepartmentId]
                        FOREIGN KEY ([DepartmentId])
                        REFERENCES [Departments] ([DepartmentId])
                        ON DELETE CASCADE;
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactType",
                table: "RelativeContacts");
        }
    }
}
