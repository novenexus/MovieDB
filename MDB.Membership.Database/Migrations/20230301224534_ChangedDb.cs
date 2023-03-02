using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MDB.Membership.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "FilmGenres",
                columns: new[] { "FilmId", "GenreId" },
                values: new object[,]
                {
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "FilmUrl", "Free", "Title" },
                values: new object[] { "A coder at a tech company wins a week-long retreat at the compound of his company's CEO, where he's tasked with testing a new artificial intelligence.", "https://www.youtube.com/embed/EoQuVnKhxaM", false, "Ex Machina" });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "FilmUrl" },
                values: new object[] { "Science, sabotage and prehistoric DNA collide when cloned dinosaurs escape their enclosures at a top-secret theme park and begin preying on the guests.", "https://www.youtube.com/embed/E8WaFvwtphY" });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "FilmUrl" },
                values: new object[] { "The contents of a hidden grave draw the interest of an industrial titan and send Officer K, an LAPD blade runner, on a quest to find a missing legend.", "https://www.youtube.com/embed/gCcx85zbxz4" });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "FilmUrl", "Free" },
                values: new object[] { "Four years after the mayhem at Jurassic Park, a research team descends upon a secret second island where the cloned dinosaurs roam free.", "https://www.youtube.com/embed/RxrvaneULkE", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                table: "FilmGenres",
                columns: new[] { "FilmId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "FilmUrl", "Free", "Title" },
                values: new object[] { "The film is set in a dystopian future Los Angeles of 2019", "https://www.youtube.com/embed/eogpIG53Cis", true, "Blade Runner" });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "FilmUrl" },
                values: new object[] { "The film is set on the fictional island of Isla Nublar", "https://www.youtube.com/embed/eogpIG53Cis" });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "FilmUrl" },
                values: new object[] { "K, an officer with the Los Angeles Police Department", "https://www.youtube.com/embed/eogpIG53Cis" });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "FilmUrl", "Free" },
                values: new object[] { "John Hammond along with few other members try to explore the Jurassic Park's second site", "https://www.youtube.com/embed/eogpIG53Cis", true });
        }
    }
}
