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

        public AuthorService(IRepository<Author, int> authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<AuthorIndexViewModel> books = await this.authorRepository
                .GetAllAttached()
                .Include(a => a.Books)
                .OrderBy(a => a.Name)
                .Select(a => new AuthorIndexViewModel
                {
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                    Books = a.Books.Select(b => new BookIndexViewModel
                    {
                        Title = b.Title
                    }).ToList() 
                })
                .ToArrayAsync();

            return books;
        }
    }
}
