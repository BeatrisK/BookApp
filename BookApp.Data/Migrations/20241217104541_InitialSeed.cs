using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling" },
                    { 2, "George R.R. Martin" },
                    { 3, "Harper Lee" },
                    { 4, "George Orwell" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Genre", "ImageUrl", "Pages", "Price", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, 1, "In the first book of the Harry Potter series, Harry discovers that he is a wizard and attends Hogwarts School of Witchcraft and Wizardry. Along with his friends Ron and Hermione, he uncovers secrets about his past and faces the dark wizard Voldemort who killed his parents. This enchanting tale filled with magical creatures, spellbinding adventures, and life-changing moments introduces the iconic characters that will shape Harry's destiny throughout the series.", "Fantasy", "https://m.media-amazon.com/images/I/81q77Q39nEL.jpg", 223, 19.99m, "Bloomsbury", "Harry Potter and the Philosopher's Stone" },
                    { 2, 1, "The second book in the Harry Potter series follows Harry as he returns to Hogwarts for his second year. A mysterious force is petrifying students, and Harry, along with his friends, investigates the legend of the Chamber of Secrets. With the discovery of ancient magical creatures and a sinister plot by the heir of Salazar Slytherin, Harry must confront new challenges that will test his bravery and loyalty.", "Fantasy", "https://m.media-amazon.com/images/I/818umIdoruL.jpg", 251, 20.99m, "Bloomsbury", "Harry Potter and the Chamber of Secrets" },
                    { 3, 2, "\"In this epic first book of the 'A Song of Ice and Fire' series, George R.R. Martin weaves a complex tale of power, betrayal, and intrigue set in the Seven Kingdoms of Westeros. As noble families vie for the Iron Throne, they are forced to confront not only each other but the dangers lurking in the north and beyond the Wall. With vivid characters and morally ambiguous choices, 'A Game of Thrones' sets the stage for a high-stakes political drama that will keep readers hooked.", "Fantasy", "https://i.harperapps.com/hcanz/covers/9780007459483/y648.jpg", 694, 25.99m, "Bantam Books", "A Game of Thrones" },
                    { 4, 2, "The second book in the 'A Song of Ice and Fire' series continues the fierce political and military struggles in Westeros. As the War of the Five Kings rages, alliances are forged and broken, and noble houses are brought to their knees. Meanwhile, darker forces stir beyond the Wall and in the east, where Daenerys Targaryen plots her return to power. With epic battles, unexpected twists, and a wide array of compelling characters.", "Fantasy", "https://target.scene7.com/is/image/Target/GUEST_7a799fff-46fc-469d-8689-cbf1e7745fef", 768, 27.99m, "Bantam Books", "A Clash of Kings" },
                    { 5, 3, "Harper Lee’s classic novel, 'To Kill a Mockingbird,' explores themes of racial injustice, moral growth, and compassion in the Deep South during the 1930s. The story is told through the eyes of Scout Finch, a young girl whose father, lawyer Atticus Finch, defends an African American man falsely accused of raping a white woman.", "Fiction", "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1612238791i/56916837.jpg", 281, 18.99m, "J.B. Lippincott & Co.", "To Kill a Mockingbird" },
                    { 6, 4, "George Orwell’s '1984' paints a chilling picture of a totalitarian regime that controls every aspect of life. The story follows Winston Smith, a worker at the Ministry of Truth, who begins to question the oppressive government that monitors all citizens through omnipresent surveillance and mind control.", "Dystopian", "https://knigomania.bg/media/catalog/product/cache/02f16ac392ba7c312a70e2f3c5d752a7/1/9/1984-9780451524935.jpg", 328, 14.99m, "Secker & Warburg", "1984" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
