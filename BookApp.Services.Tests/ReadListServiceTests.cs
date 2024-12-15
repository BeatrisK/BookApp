using BookApp.Data.Models.Repository;
using BookApp.Data.Models;
using BookApp.Services.Data;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Data.Models.Repository.Interfaces;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class ReadListServiceTests
    {
        private BookDbContext context;
        private ReadListService readListService;
        private IRepository<Book, int> bookRepository;
        private IRepository<ApplicationUserBook, object> userBookRepository;
        private IRepository<Review, int> reviewRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase("BookAppInMemoryDb")
                .Options;

            context = new BookDbContext(options);

            bookRepository = new BaseRepository<Book, int>(context);
            userBookRepository = new BaseRepository<ApplicationUserBook, object>(context);
            reviewRepository = new BaseRepository<Review, int>(context);

            readListService = new ReadListService(bookRepository, userBookRepository, reviewRepository);
        }

        [Test]
        public async Task AddBookToUserBookListAsync_ShouldReturnTrue_WhenValidDataIsProvided()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Fiction",
                Pages = 200,
                Description = "A great book to read",
                Publisher = "Test Publisher",
                Price = 20.99M,
                AuthorId = 1
            };

            var userId = "test-user-id";

            context.Books.Add(book);
            await context.SaveChangesAsync();

            var result = await readListService.AddBookToUserBookListAsync(book.Id, userId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task RemoveBookFromUserBookListAsync_ShouldReturnTrue_WhenValidDataIsProvided()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Fiction",
                Pages = 200,
                Description = "A great book to read",
                Publisher = "Test Publisher",
                Price = 20.99M,
                AuthorId = 1
            };

            var userId = "test-user-id";

            context.Books.Add(book);
            await context.SaveChangesAsync();

            await readListService.AddBookToUserBookListAsync(book.Id, userId);

            var result = await readListService.RemoveBookFromUserBookListAsync(book.Id, userId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsBookInReadListAsync_ShouldReturnTrue_WhenBookIsInReadList()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Fiction",
                Pages = 200,
                Description = "A great book to read",
                Publisher = "Test Publisher",
                Price = 20.99M,
                AuthorId = 1
            };

            var userId = "test-user-id";

            context.Books.Add(book);
            await context.SaveChangesAsync();

            await readListService.AddBookToUserBookListAsync(book.Id, userId);

            var result = await readListService.IsBookInReadListAsync(book.Id, userId);

            Assert.That(result, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }

}
