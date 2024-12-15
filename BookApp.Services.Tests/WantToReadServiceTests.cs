using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Data.Models.Repository;
using BookApp.Data.Models;
using BookApp.Data;
using BookApp.Services.Data;
using Microsoft.EntityFrameworkCore;
using BookApp.Services.Data.Interfaces;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class WantToReadServiceTests
    {
        private BookDbContext context;
        private IRepository<Book, int> bookRepository;
        private IRepository<ApplicationUserBook, object> userBookRepository;
        private IReadListService readListService;
        private IWantToReadService wantToReadService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            context = new BookDbContext(options);

            bookRepository = new BaseRepository<Book, int>(context);
            userBookRepository = new BaseRepository<ApplicationUserBook, object>(context);
            readListService = new ReadListService(bookRepository, userBookRepository, new BaseRepository<Review, int>(context));

            wantToReadService = new WantToReadService(bookRepository, readListService, userBookRepository);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Test]
        public async Task AddBookToUserWantToReadListAsync_ShouldReturnTrue_WhenValidDataIsProvided()
        {
            var userId = "user123";
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Test Genre",
                Pages = 200,
                Description = "Test Description",
                Publisher = "Test Publisher",
                Price = 19.99M,
                IsDeleted = false,
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Test Author" }
            };
            await bookRepository.AddAsync(book);

            var result = await wantToReadService.AddBookToUserWantToReadListAsync(book.Id, userId);

            Assert.That(result, Is.True);
            var userBook = await userBookRepository.FirstOrDefaultAsync(ub => ub.BookId == book.Id && ub.ApplicationUserId == userId);
            Assert.That(userBook, Is.Not.Null);
            Assert.That(userBook.IsWantToRead, Is.True);
        }

        [Test]
        public async Task IsBookInWantToReadListAsync_ShouldReturnTrue_WhenBookIsInWantToReadList()
        {
            var userId = "user123";
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Test Genre",
                Pages = 200,
                Description = "Test Description",
                Publisher = "Test Publisher",
                Price = 19.99M,
                IsDeleted = false,
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Test Author" }
            };
            await bookRepository.AddAsync(book);
            await wantToReadService.AddBookToUserWantToReadListAsync(book.Id, userId);

            var result = await wantToReadService.IsBookInWantToReadListAsync(book.Id, userId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task RemoveFromUserWantToReadListAsync_ShouldReturnTrue_WhenValidDataIsProvided()
        {
            var userId = "user123";
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Test Genre",
                Pages = 200,
                Description = "Test Description",
                Publisher = "Test Publisher",
                Price = 19.99M,
                IsDeleted = false,
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Test Author" }
            };
            await bookRepository.AddAsync(book);
            await wantToReadService.AddBookToUserWantToReadListAsync(book.Id, userId);

            var result = await wantToReadService.RemoveFromUserWantToReadListAsync(book.Id, userId);

            Assert.That(result, Is.True);
            var userBook = await userBookRepository.FirstOrDefaultAsync(ub => ub.BookId == book.Id && ub.ApplicationUserId == userId);
            Assert.That(userBook, Is.Null);
        }

        [Test]
        public async Task MarkBookAsReadAsync_ShouldReturnTrue_WhenValidDataIsProvided()
        {
            var userId = "user123";
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Test Genre",
                Pages = 200,
                Description = "Test Description",
                Publisher = "Test Publisher",
                Price = 19.99M,
                IsDeleted = false,
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Test Author" }
            };
            await bookRepository.AddAsync(book);
            await wantToReadService.AddBookToUserWantToReadListAsync(book.Id, userId);

            var result = await wantToReadService.MarkBookAsReadAsync(book.Id, userId);

            Assert.That(result, Is.True);
            var userBook = await userBookRepository.FirstOrDefaultAsync(ub => ub.BookId == book.Id && ub.ApplicationUserId == userId);
            Assert.That(userBook, Is.Not.Null);
            Assert.That(userBook.IsRead, Is.True);
        }

        [Test]
        public async Task IsBookInWantToReadListAsync_ShouldReturnFalse_WhenBookIsNotInWantToReadList()
        {
            var userId = "user123";
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Test Genre",
                Pages = 200,
                Description = "Test Description",
                Publisher = "Test Publisher",
                Price = 19.99M,
                IsDeleted = false,
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Test Author" }
            };
            await bookRepository.AddAsync(book);

            var result = await wantToReadService.IsBookInWantToReadListAsync(book.Id, userId);

            Assert.That(result, Is.False);
        }
    }
}
