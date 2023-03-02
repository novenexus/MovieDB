using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDB.Membership.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundImageUrl",
                table: "Films",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1,
                column: "BackgroundImageUrl",
                value: "/images/film1back.jpg");

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2,
                column: "BackgroundImageUrl",
                value: "/images/film2back.jpg");

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3,
                column: "BackgroundImageUrl",
                value: "/images/film3back.jpg");

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4,
                column: "BackgroundImageUrl",
                value: "/images/film4back.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImageUrl",
                table: "Films");
        }
    }
}
