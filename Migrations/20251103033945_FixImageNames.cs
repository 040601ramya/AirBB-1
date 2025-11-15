using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class FixImageNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 3,
                column: "ResidencePicture",
                value: "miami.jpeg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 3,
                column: "ResidencePicture",
                value: "miami.jpg");
        }
    }
}
