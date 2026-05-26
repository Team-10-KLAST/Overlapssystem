using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftDepartmentForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DepartmentId",
                table: "Shifts",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Departments_DepartmentId",
                table: "Shifts",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Departments_DepartmentId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_DepartmentId",
                table: "Shifts");
        }
    }
}
