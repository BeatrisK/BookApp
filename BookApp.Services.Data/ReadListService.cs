namespace BookApp.Services.Data
{
    using BookApp.Data.Models;
    using BookApp.Data.Models.Repository.Interfaces;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Lists;
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
                .Where(ub => ub.ApplicationUserId == id && ub.IsRead == true)
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

            if (book == null || userId == null)
            {
                return false;
            }

            var userBook = await userBookRepository
                .FirstOrDefaultAsync(ub => ub.BookId == bookId && ub.ApplicationUserId == userId);

            if (userBook == null)
            {
                userBook = new ApplicationUserBook
                {
                    ApplicationUserId = userId,
                    BookId = bookId,
                    IsRead = true,
                    IsWantToRead = false
                };
                await userBookRepository.AddAsync(userBook);
            }
            else
            {
                userBook.IsRead = true;
                userBook.IsWantToRead = false;
                await userBookRepository.UpdateAsync(userBook);
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

        public async Task<bool> IsBookInReadListAsync(int bookId, string userId)
        {
            var userBook = await userBookRepository
                    .FirstOrDefaultAsync(ub => ub.BookId == bookId && ub.ApplicationUserId == userId && ub.IsRead);

            return userBook != null;
        }
    }
}
