using BookApp.Data.Models;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data;
using Moq;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class AuthorServiceTests
    {
        private Mock<IRepository<Author, int>> _mockAuthorRepository;
        private Mock<IRepository<Book, int>> _mockBookRepository;
        private AuthorService _authorService;

        [SetUp]
        public void SetUp()
        {
            _mockAuthorRepository = new Mock<IRepository<Author, int>>();
            _mockBookRepository = new Mock<IRepository<Book, int>>();
            _authorService = new AuthorService(_mockAuthorRepository.Object, _mockBookRepository.Object);
        }

        [Test]
        public async Task IndexGetAllBooksOfAuthorAsync_ShouldReturnAuthorWithBooks_WhenAuthorExists()
        {
            // Arrange
            var authorId = 1;
            var author = new Author
            {
                Id = authorId,
                Name = "John Doe",
                Books = new List<Book>
            {
                new Book { Title = "Book 1", Genre = "Fiction", Pages = 300, Publisher = "Publisher 1", ImageUrl = "Image1.jpg", IsDeleted = false },
                new Book { Title = "Book 2", Genre = "Science Fiction", Pages = 400, Publisher = "Publisher 2", ImageUrl = "Image2.jpg", IsDeleted = false }
            }
            };

            // Вместо ReturnsAsync, връщаме IQueryable директно
            _mockAuthorRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Author> { author }.AsQueryable());

            // Act
            var result = await _authorService.IndexGetAllBooksOfAuthorAsync(authorId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(authorId));
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Books.Count, Is.EqualTo(2));
            Assert.That(result.Books[0].Title, Is.EqualTo("Book 1"));
            Assert.That(result.Books[1].Title, Is.EqualTo("Book 2"));
        }

        [Test]
        public async Task IndexGetAllBooksOfAuthorAsync_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorId = 999;  // Non-existent author
            _mockAuthorRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Author>().AsQueryable());  // Връщаме празен IQueryable

            // Act
            var result = await _authorService.IndexGetAllBooksOfAuthorAsync(authorId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task IndexGetAllBooksOfAuthorAsync_ShouldNotIncludeDeletedBooks()
        {
            // Arrange
            var authorId = 1;
            var author = new Author
            {
                Id = authorId,
                Name = "John Doe",
                Books = new List<Book>
            {
                new Book { Title = "Book 1", Genre = "Fiction", Pages = 300, Publisher = "Publisher 1", ImageUrl = "Image1.jpg", IsDeleted = false },
                new Book { Title = "Book 2", Genre = "Science Fiction", Pages = 400, Publisher = "Publisher 2", ImageUrl = "Image2.jpg", IsDeleted = true }
            }
            };

            _mockAuthorRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Author> { author }.AsQueryable());

            // Act
            var result = await _authorService.IndexGetAllBooksOfAuthorAsync(authorId);

            // Assert
            Assert.That(result.Books.Count, Is.EqualTo(1));  // Only one book should be included (non-deleted)
            Assert.That(result.Books[0].Title, Is.EqualTo("Book 1"));
        }
    }
}