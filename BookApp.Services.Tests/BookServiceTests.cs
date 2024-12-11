using BookApp.Data.Models;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Book;
using Moq;

namespace BookApp.Services.Tests
{ 
    [TestFixture]
    public class BookServiceTests
    {
        private Mock<IRepository<Book, int>> mockBookRepository;
        private Mock<IRepository<Author, int>> mockAuthorRepository;
        private BookService bookService;

        [SetUp]
        public void SetUp()
        {
            mockBookRepository = new Mock<IRepository<Book, int>>();
            mockAuthorRepository = new Mock<IRepository<Author, int>>();
            bookService = new BookService(mockBookRepository.Object, mockAuthorRepository.Object);
        }

        [Test]
        public async Task IndexGetAllAsync_ShouldReturnBooks_WhenBooksExist()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", IsDeleted = false, Author = new Author { Id = 1, Name = "Author 1" }},
                new Book { Id = 2, Title = "Book 2", IsDeleted = false, Author = new Author { Id = 2, Name = "Author 2" }}
            };

            mockBookRepository.Setup(repo => repo.GetAllAttached())
                .Returns(books.AsQueryable());

            // Act
            var result = await bookService.IndexGetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("Book 1"));
        }

        [Test]
        public async Task CreateBookAsync_ShouldCreateBook_WhenValidDataIsProvided()
        {
            // Arrange
            var model = new CreateBookViewModel
            {
                Title = "New Book",
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99m,
                Description = "A new book",
                Publisher = "Publisher",
                ImageUrl = "image.jpg",
                AuthorName = "Author 1"
            };

            var author = new Author { Name = "Author 1" };

            mockAuthorRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Author> { author }.AsQueryable());
            mockAuthorRepository.Setup(repo => repo.AddAsync(It.IsAny<Author>())).Returns(Task.CompletedTask);
            mockBookRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

            // Act
            await bookService.CreateBookAsync(model);

            // Assert
            mockBookRepository.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
        }

        [Test]
        public async Task GetBookDetailsByIdAsync_ShouldReturnBookDetails_WhenBookExists()
        {
            // Arrange
            var book = new Book
            {
                Id = 1,
                Title = "Book 1",
                Genre = "Fiction",
                Pages = 300,
                Description = "A great book",
                Publisher = "Publisher 1",
                Price = 20.99m,
                ImageUrl = "image.jpg",
                IsDeleted = false,
                Author = new Author { Id = 1, Name = "Author 1" }
            };

            mockBookRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Book> { book }.AsQueryable());

            // Act
            var result = await bookService.GetBookDetailsByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Book 1"));
            Assert.That(result.Author, Is.EqualTo("Author 1"));
        }

        [Test]
        public async Task EditBookAsync_ShouldEditBook_WhenValidDataIsProvided()
        {
            // Arrange
            var model = new EditBookViewModel
            {
                Id = 1,
                Title = "Updated Book",
                Genre = "Non-fiction",
                Pages = 250,
                Price = 15.99m,
                Description = "Updated description",
                Publisher = "New Publisher",
                ImageUrl = "updatedImage.jpg",
                AuthorId = 1,
                AuthorName = "Author 1"
            };

            var author = new Author { Id = 1, Name = "Author 1" };

            mockAuthorRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Author, bool>>>()))
                .ReturnsAsync(author);
            mockBookRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(true);

            // Act
            var result = await bookService.EditBookAsync(model);

            // Assert
            Assert.That(result, Is.True);
            mockBookRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Once);
        }

        [Test]
        public async Task SoftDeleteBookAsync_ShouldDeleteBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book to delete", IsDeleted = false };
            mockBookRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Book, bool>>>()))
                .ReturnsAsync(book);
            mockBookRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(true);

            // Act
            var result = await bookService.SoftDeleteBookAsync(1);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(book.IsDeleted, Is.True);
        }
    }
}