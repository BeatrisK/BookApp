using BookApp.Data.Models;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Lists;
using BookApp.Web.ViewModels.MyBook;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Data
{
    public class WantToReadService : IWantToReadService
    {
        private IRepository<Book, int> bookRepository;
        private IRepository<ApplicationUserBook, object> userBookRepository;

        public WantToReadService(IRepository<Book, int> bookRepository, IRepository<ApplicationUserBook, object> userBookRepository)
        {
            this.bookRepository = bookRepository;
            this.userBookRepository = userBookRepository;
        }

        public async Task<IEnumerable<ApplicationUserListsViewModel>> GetUserWantToReadListByIdAsync(string id)
        {
            IEnumerable<ApplicationUserListsViewModel> wantToRead = await this.userBookRepository
                .GetAllAttached()
                .Include(ub => ub.Book.Author)
                .Where(ub => ub.ApplicationUserId.ToString().ToLower() == id.ToString().ToLower())
                .Select(ub => new ApplicationUserListsViewModel()
                {
                    BookId = ub.BookId,
                    Title = ub.Book.Title,
                    Genre = ub.Book.Genre,
                    AuthorId = ub.Book.AuthorId,
                    AuthorName = ub.Book.Author.Name,
                    ImageUrl = ub.Book.ImageUrl
                })
                .ToListAsync();

            return wantToRead;
        }

        public async Task<bool> AddBookToUserWantToReadListAsync(int bookId, string userId)
        {
            Book? book = await bookRepository
                .GetByIdAsync(bookId);

            if (book == null)
            {
                return false;
            }

            if (userId == null)
            {
                return false;
            }

            ApplicationUserBook? addedToWatchlistAlready = await userBookRepository
                .FirstOrDefaultAsync(um => um.BookId == bookId &&
                                           um.ApplicationUserId == userId);

            if (addedToWatchlistAlready == null)
            {
                ApplicationUserBook newUserMovie = new ApplicationUserBook()
                {
                    ApplicationUserId = userId,
                    BookId = bookId
                };

                await this.userBookRepository
                     .AddAsync(newUserMovie);
            }

            return true;
        }


        public async Task<bool> RemoveFromUserWantToReadListAsync(int bookId, string userId)
        {
            Book? movie = await this.bookRepository
                .GetByIdAsync(bookId);

            if (movie == null)
            {
                return false;
            }

            ApplicationUserBook? applicationUserBook = await userBookRepository
                .FirstOrDefaultAsync(ub => ub.BookId == bookId &&
                                           ub.ApplicationUserId == userId);

            if (applicationUserBook != null)
            {
                await this.userBookRepository.DeleteAsync(applicationUserBook);
            }

            return true;
        }
    }
}
