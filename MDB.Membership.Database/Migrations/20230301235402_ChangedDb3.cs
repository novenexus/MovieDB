using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MDB.Membership.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "Description", "Name" },
                values: new object[] { null, null, "Alex Garland" });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Avatar", "Description", "Name" },
                values: new object[,]
                {
                    { 2, null, null, "Steven Spielberg" },
                    { 3, null, null, "Denis Villeneuve" },
                    { 4, null, null, "Steven Spielberg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "Description", "Name" },
                values: new object[] { "/images/director1.jpg", "One of the most influential personalities in the history of cinema, Steven Spielberg is Hollywood's best known director", "Director Name" });
        }
    }
}
