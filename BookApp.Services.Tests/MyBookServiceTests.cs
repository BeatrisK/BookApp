using BookApp.Data;
using BookApp.Data.Models;
using BookApp.Data.Models.Repository;
using BookApp.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class MyBookServiceTests
    {
        private BookDbContext context;
        private MyBookService myBookService;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: $"BookDb_{Guid.NewGuid()}")
                .Options;

            context = new BookDbContext(options);

            var author = new Author
            {
                Id = 1,
                Name = "John Doe"
            };

            var books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book One",
                Genre = "Fiction",
                Pages = 300,
                Description = "A fascinating tale.",
                Publisher = "Publisher A",
                Price = 19.99M,
                ImageUrl = null,
                Author = author
            },
            new Book
            {
                Id = 2,
                Title = "Book Two",
                Genre = "Non-Fiction",
                Pages = 200,
                Description = "An informative book.",
                Publisher = "Publisher B",
                Price = 9.99M,
                ImageUrl = null,
                Author = author
            }
        };

            context.Authors.Add(author);
            context.Books.AddRange(books);

            await context.SaveChangesAsync();

            var bookRepository = new BaseRepository<Book, int>(context);
            myBookService = new MyBookService(bookRepository);
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
        public async Task IndexGetAllAsync_ShouldReturnAllBooksOrderedByTitle()
        {
            var result = (await myBookService.IndexGetAllAsync()).ToList();

            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 books in the result.");
            Assert.That(result[0].Title, Is.EqualTo("Book One"), "Expected the first book to be 'Book One'.");
            Assert.That(result[1].Title, Is.EqualTo("Book Two"), "Expected the second book to be 'Book Two'.");
        }

        [Test]
        public async Task IndexGetAllAsync_ShouldReturnEmptyList_WhenNoBooksExist()
        {
            var allBooks = context.Books.ToList();
            context.Books.RemoveRange(allBooks);
            await context.SaveChangesAsync();

            var result = (await myBookService.IndexGetAllAsync()).ToList();

            Assert.That(result.Count, Is.EqualTo(0), "Expected no books in the result.");
        }
    }
}