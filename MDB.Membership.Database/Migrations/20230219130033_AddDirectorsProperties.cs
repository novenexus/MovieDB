using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDB.Membership.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectorsProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "Description" },
                values: new object[] { "/images/director1.jpg", "One of the most influential personalities in the history of cinema, Steven Spielberg is Hollywood's best known director" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Directors");
        }
    }
}
