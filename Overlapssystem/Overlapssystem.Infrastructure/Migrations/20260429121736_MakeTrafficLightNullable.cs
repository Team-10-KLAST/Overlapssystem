using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class MakeTrafficLightNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "TrafficLight",
            table: "ResidentSchemas",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "TrafficLight",
            table: "ResidentSchemas",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);
    }
}
