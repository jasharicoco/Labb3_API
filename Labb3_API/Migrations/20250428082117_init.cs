using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3_API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Links",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Cykling.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Cykelturer.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Bergsklättring.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "KlättraBerg.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Ving.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Apollo.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Flygresor.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Meditation.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Fotboll.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Matlagning.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Astro.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Konst.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Film.se");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Github.com");

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "StackOverflow.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Links");
        }
    }
}
