using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddDepartmentNoteForeignKey : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddForeignKey(
            name: "FK_DepartmentNotes_Departments_DepartmentId",
            table: "DepartmentNotes",
            column: "DepartmentId",
            principalTable: "Departments",
            principalColumn: "DepartmentId",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_DepartmentNotes_Departments_DepartmentId",
            table: "DepartmentNotes");
    }
}
