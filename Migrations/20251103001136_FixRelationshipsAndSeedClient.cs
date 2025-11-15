using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationshipsAndSeedClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_ClientUserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Residences_ResidenceId1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Residences_Locations_LocationId1",
                table: "Residences");

            migrationBuilder.DropIndex(
                name: "IX_Residences_LocationId1",
                table: "Residences");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ClientUserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ResidenceId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ResidenceId1",
                table: "Reservations");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "UserId", "DOB", "Email", "Name", "PhoneNumber" },
                values: new object[] { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "demo@example.com", "Demo User", "000-000-0000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "LocationId1",
                table: "Residences",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientUserId",
                table: "Reservations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResidenceId1",
                table: "Reservations",
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

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientUserId",
                table: "Reservations",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ResidenceId1",
                table: "Reservations",
                column: "ResidenceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_ClientUserId",
                table: "Reservations",
                column: "ClientUserId",
                principalTable: "Clients",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Residences_ResidenceId1",
                table: "Reservations",
                column: "ResidenceId1",
                principalTable: "Residences",
                principalColumn: "ResidenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Residences_Locations_LocationId1",
                table: "Residences",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "LocationId");
        }
    }
}
