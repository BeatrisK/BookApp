using BookApp.Data;
using BookApp.Data.Models;
using BookApp.Data.Models.Repository;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Book;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class BookServiceUnitTests
    {
        private BookDbContext context;
        private IBookService bookService;
        private IRepository<Book, int> bookRepository;
        private IRepository<Author, int> authorRepository;

        private string authorName = "J.K. Rowling";

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "BookInMemoryDb" + Guid.NewGuid().ToString())
                .Options;

            context = new BookDbContext(options);
            bookRepository = new BaseRepository<Book, int>(context);
            authorRepository = new BaseRepository<Author, int>(context);

            bookService = new BookService(bookRepository, authorRepository);

            var author = new Author { Name = authorName };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var book = new Book
            {
                Title = "Harry Potter and the Philosopher's Stone",
                Genre = "Fantasy",
                Pages = 223,
                Price = 19.99m,
                Description = "A fantasy novel",
                Publisher = "Bloomsbury",
                ImageUrl = "image_url",
                Author = author
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();
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
        public async Task IndexGetAllAsync_ShouldReturnBooks_WhenBooksExist()
        {
            var books = await bookService.IndexGetAllAsync();

            Assert.That(books.Count(), Is.EqualTo(1));
            Assert.That(books.First().Title, Is.EqualTo("Harry Potter and the Philosopher's Stone"));
            Assert.That(books.First().AuthorName, Is.EqualTo(authorName));
        }

        [Test]
        public async Task CreateBookAsync_ShouldAddNewBook_WhenValidModelIsProvided()
        {
            var createBookModel = new CreateBookViewModel
            {
                Title = "Harry Potter and the Chamber of Secrets",
                Genre = "Fantasy",
                Pages = 251,
                Price = 21.99m,
                Description = "Second book in the Harry Potter series",
                Publisher = "Bloomsbury",
                AuthorName = authorName,
                ImageUrl = "image_url_2"
            };

            // Act
            await bookService.CreateBookAsync(createBookModel);

            // Assert
            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Title == createBookModel.Title);

            Assert.NotNull(book);
            Assert.That(book.Title, Is.EqualTo(createBookModel.Title));
            Assert.That(book.Author.Name, Is.EqualTo(authorName));
        }

        [Test]
        public async Task GetBookDetailsByIdAsync_ShouldReturnBookDetails_WhenBookExists()
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Title == "Harry Potter and the Philosopher's Stone");

            var bookDetails = await bookService.GetBookDetailsByIdAsync(book.Id);

            Assert.NotNull(bookDetails);
            Assert.That(bookDetails.Title, Is.EqualTo(book.Title));
            Assert.That(bookDetails.Author, Is.EqualTo(book.Author.Name));
        }

        [Test]
        public async Task EditBookAsync_ShouldUpdateBookDetails_WhenValidModelIsProvided()
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Title == "Harry Potter and the Philosopher's Stone");
            var editModel = new EditBookViewModel
            {
                Id = book.Id,
                Title = "Harry Potter and the Sorcerer's Stone",
                Genre = "Fantasy",
                Pages = 223,
                Price = 19.99m,
                Description = "Updated description",
                Publisher = "Bloomsbury",
                ImageUrl = "updated_image_url",
                AuthorId = book.AuthorId,
                AuthorName = book.Author.Name
            };

            var result = await bookService.EditBookAsync(editModel);

            Assert.IsTrue(result);
            var updatedBook = await context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            Assert.That(updatedBook.Title, Is.EqualTo(editModel.Title));
        }

        [Test]
        public async Task SoftDeleteBookAsync_ShouldMarkBookAsDeleted_WhenBookExists()
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Title == "Harry Potter and the Philosopher's Stone");

            var result = await bookService.SoftDeleteBookAsync(book.Id);

            Assert.IsTrue(result);
            var deletedBook = await context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            Assert.That(deletedBook.IsDeleted, Is.True);
        }

        [Test]
        public async Task SoftDeleteBookAsync_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            var result = await bookService.SoftDeleteBookAsync(999);

            Assert.IsFalse(result);
        }
    }
}