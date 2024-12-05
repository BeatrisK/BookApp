using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WantToReadList_AspNetUsers_UserId1",
                table: "WantToReadList");

            migrationBuilder.DropIndex(
                name: "IX_WantToReadList_UserId1",
                table: "WantToReadList");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "WantToReadList");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WantToReadList",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WantToReadList_UserId",
                table: "WantToReadList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WantToReadList_AspNetUsers_UserId",
                table: "WantToReadList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WantToReadList_AspNetUsers_UserId",
                table: "WantToReadList");

            migrationBuilder.DropIndex(
                name: "IX_WantToReadList_UserId",
                table: "WantToReadList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WantToReadList",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "WantToReadList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WantToReadList_UserId1",
                table: "WantToReadList",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WantToReadList_AspNetUsers_UserId1",
                table: "WantToReadList",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
