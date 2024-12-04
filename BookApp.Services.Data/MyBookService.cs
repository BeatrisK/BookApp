namespace BookApp.Services.Data
{
    using BookApp.Data.Models;
    using BookApp.Data.Models.Repository.Interfaces;
    using BookApp.Services.Data.Interfaces;
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
    }
}
