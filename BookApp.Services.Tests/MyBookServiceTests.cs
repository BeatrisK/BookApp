using BookApp.Data.Models;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data;
using Moq;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class MyBookServiceTests
    {
        private Mock<IRepository<Book, int>> _mockBookRepository;
        private MyBookService _bookService;

        [SetUp]
        public void SetUp()
        {
            // Мокиране на репозиторието
            _mockBookRepository = new Mock<IRepository<Book, int>>();
            _bookService = new MyBookService(_mockBookRepository.Object);
        }

        [Test]
        public async Task IndexGetAllAsync_ShouldReturnOrderedBooks_WhenCalled()
        {
            // Подготвяме тестови данни
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book A", Author = new Author { Name = "Author Z" } },
                new Book { Id = 2, Title = "Book B", Author = new Author { Name = "Author Y" } },
                new Book { Id = 3, Title = "Book C", Author = new Author { Name = "Author X" } }
            };

            // Мок на GetAllAttached() метод, който ще върне нашия списък с книги
            _mockBookRepository.Setup(repo => repo.GetAllAttached())
                .Returns(books.AsQueryable()); // Връщаме IQueryable, без да използваме ReturnsAsync

            // Извикваме метода на сервиза
            var result = await _bookService.IndexGetAllAsync();

            // Преобразуваме резултата в List, за да използваме Count
            var resultList = result.ToList();

            // Проверяваме дали броят на книгите е правилен
            Assert.That(resultList.Count, Is.EqualTo(3)); // Проверка на броя елементи

            // Проверяваме дали книгите са подредени правилно по заглавие
            Assert.That(resultList[0].Title, Is.EqualTo("Book A"));
            Assert.That(resultList[1].Title, Is.EqualTo("Book B"));
            Assert.That(resultList[2].Title, Is.EqualTo("Book C"));
        }
    }
}