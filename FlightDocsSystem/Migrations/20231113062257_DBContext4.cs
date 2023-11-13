using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocsSystem.Migrations
{
    /// <inheritdoc />
    public partial class DBContext4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "FlightJourneys",
                columns: table => new
                {
                    JourneyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    JourneyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightJourneys", x => x.JourneyId);
                    table.ForeignKey(
                        name: "FK_FlightJourneys_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightJourneys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightJourneys_FlightId",
                table: "FlightJourneys",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightJourneys_UserId",
                table: "FlightJourneys",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightJourneys");

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
    }
}
