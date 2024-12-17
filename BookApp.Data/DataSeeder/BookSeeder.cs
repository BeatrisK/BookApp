using BookApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Data.DataSeeder
{
    public class BookSeeder
    {
        public static void SeedBooks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling" },
                new Author { Id = 2, Name = "George R.R. Martin" },
                new Author { Id = 3, Name = "Harper Lee" },
                new Author { Id = 4, Name = "George Orwell" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    AuthorId = 1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "In the first book of the Harry Potter series, Harry discovers that he is a wizard and attends Hogwarts School of Witchcraft and Wizardry. Along with his friends Ron and Hermione, he uncovers secrets about his past and faces the dark wizard Voldemort who killed his parents. This enchanting tale filled with magical creatures, spellbinding adventures, and life-changing moments introduces the iconic characters that will shape Harry's destiny throughout the series.",
                    Genre = "Fantasy",
                    ImageUrl = "https://m.media-amazon.com/images/I/81q77Q39nEL.jpg",
                    Pages = 223,
                    Price = 19.99m,
                    Publisher = "Bloomsbury"
                },
                new Book
                {
                    Id = 2,
                    AuthorId = 1,
                    Title = "Harry Potter and the Chamber of Secrets",
                    Description = "The second book in the Harry Potter series follows Harry as he returns to Hogwarts for his second year. A mysterious force is petrifying students, and Harry, along with his friends, investigates the legend of the Chamber of Secrets. With the discovery of ancient magical creatures and a sinister plot by the heir of Salazar Slytherin, Harry must confront new challenges that will test his bravery and loyalty.",
                    Genre = "Fantasy",
                    ImageUrl = "https://m.media-amazon.com/images/I/818umIdoruL.jpg",
                    Pages = 251,
                    Price = 20.99m,
                    Publisher = "Bloomsbury"
                },
                new Book
                {
                    Id = 3,
                    AuthorId = 2,
                    Title = "A Game of Thrones",
                    Description = "\"In this epic first book of the 'A Song of Ice and Fire' series, George R.R. Martin weaves a complex tale of power, betrayal, and intrigue set in the Seven Kingdoms of Westeros. As noble families vie for the Iron Throne, they are forced to confront not only each other but the dangers lurking in the north and beyond the Wall. With vivid characters and morally ambiguous choices, 'A Game of Thrones' sets the stage for a high-stakes political drama that will keep readers hooked.",
                    Genre = "Fantasy",
                    ImageUrl = "https://i.harperapps.com/hcanz/covers/9780007459483/y648.jpg",
                    Pages = 694,
                    Price = 25.99m,
                    Publisher = "Bantam Books"
                },
                new Book
                {
                    Id = 4,
                    AuthorId = 2,
                    Title = "A Clash of Kings",
                    Description = "The second book in the 'A Song of Ice and Fire' series continues the fierce political and military struggles in Westeros. As the War of the Five Kings rages, alliances are forged and broken, and noble houses are brought to their knees. Meanwhile, darker forces stir beyond the Wall and in the east, where Daenerys Targaryen plots her return to power. With epic battles, unexpected twists, and a wide array of compelling characters.",
                    Genre = "Fantasy",
                    ImageUrl = "https://target.scene7.com/is/image/Target/GUEST_7a799fff-46fc-469d-8689-cbf1e7745fef",
                    Pages = 768,
                    Price = 27.99m,
                    Publisher = "Bantam Books"
                },
                new Book
                {
                    Id = 5,
                    AuthorId = 3,
                    Title = "To Kill a Mockingbird",
                    Description = "Harper Lee’s classic novel, 'To Kill a Mockingbird,' explores themes of racial injustice, moral growth, and compassion in the Deep South during the 1930s. The story is told through the eyes of Scout Finch, a young girl whose father, lawyer Atticus Finch, defends an African American man falsely accused of raping a white woman.",
                    Genre = "Fiction",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1612238791i/56916837.jpg",
                    Pages = 281,
                    Price = 18.99m,
                    Publisher = "J.B. Lippincott & Co."
                },
                new Book
                {
                    Id = 6,
                    AuthorId = 4,
                    Title = "1984",
                    Description = "George Orwell’s '1984' paints a chilling picture of a totalitarian regime that controls every aspect of life. The story follows Winston Smith, a worker at the Ministry of Truth, who begins to question the oppressive government that monitors all citizens through omnipresent surveillance and mind control.",
                    Genre = "Dystopian",
                    ImageUrl = "https://knigomania.bg/media/catalog/product/cache/02f16ac392ba7c312a70e2f3c5d752a7/1/9/1984-9780451524935.jpg",
                    Pages = 328,
                    Price = 14.99m,
                    Publisher = "Secker & Warburg"
                }
            );
        }
    }
}
