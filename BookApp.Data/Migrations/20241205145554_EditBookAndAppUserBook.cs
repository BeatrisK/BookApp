using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditBookAndAppUserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsWantToRead",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "UsersBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWantToRead",
                table: "UsersBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "UsersBooks");

            migrationBuilder.DropColumn(
                name: "IsWantToRead",
                table: "UsersBooks");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWantToRead",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
