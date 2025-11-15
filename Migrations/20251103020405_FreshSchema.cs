using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class FreshSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckOut",
                table: "Reservations",
                newName: "ReservationStartDate");

            migrationBuilder.RenameColumn(
                name: "CheckIn",
                table: "Reservations",
                newName: "ReservationEndDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservations",
                newName: "ReservationId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ReservationStartDate",
                table: "Reservations",
                newName: "CheckOut");

            migrationBuilder.RenameColumn(
                name: "ReservationEndDate",
                table: "Reservations",
                newName: "CheckIn");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Reservations",
                newName: "Id");
        }
    }
}
