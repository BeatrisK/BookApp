using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadList_AspNetUsers_UserId",
                table: "ReadList");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadList_Books_BookId",
                table: "ReadList");

            migrationBuilder.DropForeignKey(
                name: "FK_WantToReadList_AspNetUsers_UserId",
                table: "WantToReadList");

            migrationBuilder.DropForeignKey(
                name: "FK_WantToReadList_Books_BookId",
                table: "WantToReadList");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WantToReadList",
                table: "WantToReadList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadList",
                table: "ReadList");

            migrationBuilder.RenameTable(
                name: "WantToReadList",
                newName: "WantToReadLists");

            migrationBuilder.RenameTable(
                name: "ReadList",
                newName: "ReadLists");

            migrationBuilder.RenameIndex(
                name: "IX_WantToReadList_UserId",
                table: "WantToReadLists",
                newName: "IX_WantToReadLists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WantToReadList_BookId",
                table: "WantToReadLists",
                newName: "IX_WantToReadLists_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadList_UserId",
                table: "ReadLists",
                newName: "IX_ReadLists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadList_BookId",
                table: "ReadLists",
                newName: "IX_ReadLists_BookId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WantToReadLists",
                table: "WantToReadLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadLists",
                table: "ReadLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadLists_AspNetUsers_UserId",
                table: "ReadLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadLists_Books_BookId",
                table: "ReadLists",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantToReadLists_AspNetUsers_UserId",
                table: "WantToReadLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantToReadLists_Books_BookId",
                table: "WantToReadLists",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadLists_AspNetUsers_UserId",
                table: "ReadLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadLists_Books_BookId",
                table: "ReadLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WantToReadLists_AspNetUsers_UserId",
                table: "WantToReadLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WantToReadLists_Books_BookId",
                table: "WantToReadLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WantToReadLists",
                table: "WantToReadLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadLists",
                table: "ReadLists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "WantToReadLists",
                newName: "WantToReadList");

            migrationBuilder.RenameTable(
                name: "ReadLists",
                newName: "ReadList");

            migrationBuilder.RenameIndex(
                name: "IX_WantToReadLists_UserId",
                table: "WantToReadList",
                newName: "IX_WantToReadList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WantToReadLists_BookId",
                table: "WantToReadList",
                newName: "IX_WantToReadList_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadLists_UserId",
                table: "ReadList",
                newName: "IX_ReadList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadLists_BookId",
                table: "ReadList",
                newName: "IX_ReadList_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WantToReadList",
                table: "WantToReadList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadList",
                table: "ReadList",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Shelves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ReadList_AspNetUsers_UserId",
                table: "ReadList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadList_Books_BookId",
                table: "ReadList",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantToReadList_AspNetUsers_UserId",
                table: "WantToReadList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantToReadList_Books_BookId",
                table: "WantToReadList",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
