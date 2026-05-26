using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RemoveContactType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ContactType",
            table: "RelativeContacts");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "ContactType",
            table: "RelativeContacts",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}
