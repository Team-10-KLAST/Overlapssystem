using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentToShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Shifts");

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Shifts");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
