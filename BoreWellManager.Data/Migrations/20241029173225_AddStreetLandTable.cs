using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoreWellManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStreetLandTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Lands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "Lands");
        }
    }
}
