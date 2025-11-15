using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class FreshAlignedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residences_Locations_LocationId1",
                table: "Residences");

            migrationBuilder.DropIndex(
                name: "IX_Residences_LocationId1",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Residences");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId1",
                table: "Residences",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 1,
                column: "LocationId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 2,
                column: "LocationId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 3,
                column: "LocationId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Residences_LocationId1",
                table: "Residences",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Residences_Locations_LocationId1",
                table: "Residences",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "LocationId");
        }
    }
}
