namespace BookApp.Services.Data
{
    using BookApp.Data.Models;
    using BookApp.Data.Models.Repository.Interfaces;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Lists;
    using BookApp.Web.ViewModels.MyBook;
    using Microsoft.EntityFrameworkCore;

    public class ReadListService : IReadListService
    {
        private IRepository<Book, int> bookRepository;
        private IRepository<ApplicationUserBook, object> userBookRepository;

        public ReadListService(IRepository<Book, int> bookRepository, IRepository<ApplicationUserBook, object> userBookRepository)
        {
            this.bookRepository = bookRepository;       
            this.userBookRepository = userBookRepository;
        }

        public async Task<IEnumerable<ApplicationUserListsViewModel>> GetUserReadListByIdAsync(string id)
        {
            IEnumerable<ApplicationUserListsViewModel> readBooksList = await this.userBookRepository
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

            return readBooksList;
        }

        public async Task<bool> AddBookToUserBookListAsync(int bookId, string userId)
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

            ApplicationUserBook? addedToReadListAlready = await userBookRepository
                .FirstOrDefaultAsync(um => um.BookId == bookId &&
                                           um.ApplicationUserId == userId);

            if (addedToReadListAlready == null)
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
  
        public async Task<bool> RemoveBookFromUserBookListAsync(int bookId, string userId)
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
