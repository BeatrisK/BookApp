namespace BookApp.Services.Data
{
    using BookApp.Data.Models;
    using BookApp.Data.Models.Repository.Interfaces;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Book;
    using BookApp.Web.ViewModels.MyBook;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MyBookService : IMyBookService
    {
        private IRepository<Book, int> bookRepository;

        public MyBookService(IRepository<Book, int> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<IndexMyBookViewModel>> IndexGetAllAsync()
        {
            IEnumerable<IndexMyBookViewModel> books = await this.bookRepository
                .GetAllAttached()
                .OrderBy(b => b.Title)
                .Select(b => new IndexMyBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name
                })
                .ToArrayAsync();

            return books;
        }
        public Task<bool> AddBookToUserBookListAsync(string? bookId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserWantToReadListAsync(string? bookId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserReadBooksViewModel>> GetUserBookListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserReadBooksViewModel>> GetUserWantToReadListkByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveBookFromUserBookAsync(string? bookId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromUserWantToReadListAsync(string? bookId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
