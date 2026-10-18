using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineFlow.Catalog.Api.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsAndConfigureHallRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hall_seats");

            migrationBuilder.DropTable(
                name: "screenings");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "movies",
                newName: "movie_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "halls",
                newName: "hall_id");

            migrationBuilder.CreateTable(
                name: "seats",
                columns: table => new
                {
                    seat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hall_id = table.Column<Guid>(type: "uuid", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seats", x => x.seat_id);
                    table.ForeignKey(
                        name: "FK_seats_halls_hall_id",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "hall_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "showtime",
                columns: table => new
                {
                    showtime_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hall_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showtime", x => x.showtime_id);
                    table.ForeignKey(
                        name: "FK_showtime_halls_hall_id",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "hall_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_showtime_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_seats_hall_id_row_number",
                table: "seats",
                columns: new[] { "hall_id", "row", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_showtime_hall_id_start_time",
                table: "showtime",
                columns: new[] { "hall_id", "start_time" });

            migrationBuilder.CreateIndex(
                name: "IX_showtime_movie_id",
                table: "showtime",
                column: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "seats");

            migrationBuilder.DropTable(
                name: "showtime");

            migrationBuilder.RenameColumn(
                name: "movie_id",
                table: "movies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "hall_id",
                table: "halls",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "hall_seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    hall_id = table.Column<Guid>(type: "uuid", nullable: false),
                    seat_number = table.Column<int>(type: "integer", nullable: false),
                    row_number = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hall_seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hall_seats_halls_hall_id",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "screenings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hall_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_screenings_halls_hall_id",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_screenings_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hall_seats_hall_id_row_number_seat_number",
                table: "hall_seats",
                columns: new[] { "hall_id", "row_number", "seat_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_screenings_hall_id_start_time",
                table: "screenings",
                columns: new[] { "hall_id", "start_time" });

            migrationBuilder.CreateIndex(
                name: "IX_screenings_movie_id",
                table: "screenings",
                column: "movie_id");
        }
    }
}
