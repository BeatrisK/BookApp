using BookApp.Data.Models;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Book;
using BookApp.Data.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Data
{
    public class BookService : IBookService
    {
        private IRepository<Book, int> bookRepository;

        public BookService(IRepository<Book, int> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task CreateBookAsync(CreateBookViewModel model)
        {
            Book book = new Book
            {
                Title = model.Title,
                Genre = model.Genre,
                Pages = model.Pages,
                Price = model.Price,
                Description = model.Description,
                Publisher = model.Publisher,
                ImageUrl = model.ImageUrl
            };

            if (book.Author == null)
            {
                book.Author = new Author();  
            }

            book.Author.Name = model.AuthorName;

            await this.bookRepository.AddAsync(book);
        }

        public async Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int id)
        {
            Book? book = await this.bookRepository
                .GetAllAttached()
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            BookDetailsViewModel? viewModel = null;

            if (book != null)
            {
                viewModel = new BookDetailsViewModel()
                {
                    Id = id,
                    Title = book.Title,
                    Genre = book.Genre,
                    Pages = book.Pages,
                    Description = book.Description,
                    Publisher = book.Publisher,
                    Price= book.Price,
                    ImageUrl = book.ImageUrl,
                    Author = book.Author.Name
                };
            }

            return viewModel;
        }

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<BookIndexViewModel> books = await this.bookRepository
                .GetAllAttached()
                .OrderBy(b => b.Title)
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name,
                    ImageUrl= b.ImageUrl
                })
                .ToArrayAsync();

            return books;
        }
    }
}
