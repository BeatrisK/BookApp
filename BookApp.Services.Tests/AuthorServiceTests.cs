using BookApp.Data.Models;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Data.Models.Repository;
using BookApp.Services.Data.Interfaces;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class AuthorServiceUnitTests
    {
        private BookDbContext context;
        private IAuthorService authorService;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "BookDbTest" + Guid.NewGuid().ToString())
                .Options;

            context = new BookDbContext(options);

            var author = new Author
            {
                Id = 1,
                Name = "J.K. Rowling",
                Books = new List<Book>
                {
                    new Book
                    {
                        Id = 1,
                        Title = "Harry Potter and the Sorcerer's Stone",
                        Genre = "Fantasy",
                        Pages = 320,
                        Publisher = "Bloomsbury",
                        ImageUrl = "url1",
                        IsDeleted = false,
                        Description = "A young wizard's first year at Hogwarts.",
                        Price = 20.99m,
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "Harry Potter and the Chamber of Secrets",
                        Genre = "Fantasy",
                        Pages = 341,
                        Publisher = "Bloomsbury",
                        ImageUrl = "url2",
                        IsDeleted = false,
                        Description = "The second year of Harry Potter at Hogwarts.",
                        Price = 21.99m,
                    }
                }
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var authorRepository = new BaseRepository<Author, int>(context);
            var bookRepository = new BaseRepository<Book, int>(context);
            authorService = new AuthorService(authorRepository, bookRepository);
        }

        [TearDown]
        public async Task Teardown()
        {
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.DisposeAsync();
            }
        }

        [Test]
        public async Task IndexGetAllBooksOfAuthorAsync_ShouldFilterOutDeletedBooks()
        {
            var authorWithDeletedBooks = new Author
            {
                Id = 2,
                Name = "Ernest Hemingway",
                Books = new List<Book>
        {
            new Book
            {
                Id = 3,
                Title = "The Old Man and the Sea",
                Genre = "Fiction",
                Pages = 127,
                Publisher = "Scribner",
                ImageUrl = "url3",
                IsDeleted = true,
                Description = "A short novel about an old fisherman.",
                Price = 15.99m,
            },
            new Book
            {
                Id = 4,
                Title = "A Farewell to Arms",
                Genre = "Fiction",
                Pages = 355,
                Publisher = "Scribner",
                ImageUrl = "url4",
                IsDeleted = false,
                Description = "A novel set during World War I.",
                Price = 18.99m,
            }
        }
            };

            context.Authors.Add(authorWithDeletedBooks);
            await context.SaveChangesAsync();

            var result = await authorService.IndexGetAllBooksOfAuthorAsync(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Books.Count);
            Assert.AreEqual("A Farewell to Arms", result.Books[0].Title);
        }
    }
}

