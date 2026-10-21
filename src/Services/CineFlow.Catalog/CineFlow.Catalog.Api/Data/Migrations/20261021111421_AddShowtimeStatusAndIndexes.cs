using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineFlow.Catalog.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShowtimeStatusAndIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "showtime",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "Scheduled");

            migrationBuilder.CreateIndex(
                name: "IX_showtime_status_end_time",
                table: "showtime",
                columns: new[] { "status", "end_time" });

            migrationBuilder.CreateIndex(
                name: "IX_showtime_status_start_time",
                table: "showtime",
                columns: new[] { "status", "start_time" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_showtime_status_end_time",
                table: "showtime");

            migrationBuilder.DropIndex(
                name: "IX_showtime_status_start_time",
                table: "showtime");

            migrationBuilder.DropColumn(
                name: "status",
                table: "showtime");
        }
    }
}
