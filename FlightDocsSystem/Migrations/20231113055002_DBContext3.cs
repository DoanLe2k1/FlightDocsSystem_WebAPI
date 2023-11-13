using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocsSystem.Migrations
{
    /// <inheritdoc />
    public partial class DBContext3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JourneyFlightId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JourneyFlights",
                columns: table => new
                {
                    JourneyFlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyFlights", x => x.JourneyFlightId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_JourneyFlightId",
                table: "Users",
                column: "JourneyFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_JourneyFlights_JourneyFlightId",
                table: "Users",
                column: "JourneyFlightId",
                principalTable: "JourneyFlights",
                principalColumn: "JourneyFlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_JourneyFlights_JourneyFlightId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "JourneyFlights");

            migrationBuilder.DropIndex(
                name: "IX_Users_JourneyFlightId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JourneyFlightId",
                table: "Users");
        }
    }
}
