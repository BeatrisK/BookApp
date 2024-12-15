using BookApp.Data.Models;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Author;
using BookApp.Web.ViewModels.Book;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Data
{
    public class AuthorService : IAuthorService
    {
        private IRepository<Author, int> authorRepository;
        private IRepository<Book, int> bookRepository;

        public AuthorService(IRepository<Author, int> authorRepository, IRepository<Book, int> bookRepository)
        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<AuthorIndexViewModel> IndexGetAllBooksOfAuthorAsync(int id)
        {
            Author? author = await authorRepository
                .GetAllAttached()
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            AuthorIndexViewModel viewModel = null;

            if (author != null)
            {
                viewModel = new AuthorIndexViewModel
                {
                    Id = author.Id,
                    Name = author.Name,
                    Books = author.Books
                    .Where(b => !b.IsDeleted)
                    .Select(b => new AuthorPageBookDetailsViewModel
                    {
                        Title = b.Title,
                        Genre = b.Genre,
                        Pages = b.Pages,
                        Publisher = b.Publisher,
                        ImageUrl = b.ImageUrl
                    }).ToList()
                };
            }

            return viewModel;
        }
    }
}
