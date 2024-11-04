using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoreWellManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationBetweenDocAndWell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Lands_LandId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_OwnerId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_LandId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_OwnerId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "LandId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Documents",
                newName: "WellId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_WellId",
                table: "Documents",
                column: "WellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Wells_WellId",
                table: "Documents",
                column: "WellId",
                principalTable: "Wells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Wells_WellId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_WellId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "WellId",
                table: "Documents",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "LandId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LandId",
                table: "Documents",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OwnerId",
                table: "Documents",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Lands_LandId",
                table: "Documents",
                column: "LandId",
                principalTable: "Lands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_OwnerId",
                table: "Documents",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
