using BookApp.Data.Models;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Lists;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Data
{
    public class WantToReadService : IWantToReadService
    {
        private IRepository<Book, int> bookRepository;
        private readonly IReadListService readListService;
        private IRepository<ApplicationUserBook, object> userBookRepository;

        public WantToReadService(IRepository<Book, int> bookRepository, IReadListService readListService, IRepository<ApplicationUserBook, object> userBookRepository)
        {
            this.bookRepository = bookRepository;
            this.readListService = readListService;
            this.userBookRepository = userBookRepository;
        }

        public async Task<IEnumerable<ApplicationUserListsViewModel>> GetUserWantToReadListByIdAsync(string id)
        {
            IEnumerable<ApplicationUserListsViewModel> wantToRead = await this.userBookRepository
                .GetAllAttached()
                .Include(ub => ub.Book.Author)
                .Where(ub => ub.ApplicationUserId == id && ub.IsWantToRead == true)
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
            var isInReadList = await this.readListService.IsBookInReadListAsync(bookId, userId);
            if (isInReadList)
            {
                await MoveBookToWantToReadListAsync(bookId, userId);
                return true;
            }

            var isInWantToReadList = await IsBookInWantToReadListAsync(bookId, userId);
            if (isInWantToReadList)
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
                    IsWantToRead = true,
                    IsRead = false
                };
                await userBookRepository.AddAsync(userBook);
            }
            else
            {
                userBook.IsWantToRead = true;
                await userBookRepository.UpdateAsync(userBook);
            }

            return true; /*
            if (await this.readListService.IsBookInReadListAsync(bookId, userId))
            {
                return false;
            }

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
                userBook = new ApplicationUserBook()
                {
                    ApplicationUserId = userId,
                    BookId = bookId,
                    IsRead = false,
                    IsWantToRead = true
                };

                await this.userBookRepository
                     .AddAsync(userBook);
            }
            else
            {
                userBook.IsRead = false;
                userBook.IsWantToRead = true;
                await userBookRepository.UpdateAsync(userBook);
            }

            return true;*/
        }

        public async Task<bool> RemoveFromUserWantToReadListAsync(int bookId, string userId)
        {
            Book? book = await this.bookRepository
                .GetByIdAsync(bookId);

            if (book == null)
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

        public async Task<bool> MarkBookAsReadAsync(int bookId, string userId)
        {
            Book? book = await bookRepository
                .GetByIdAsync(bookId);

            if (book == null || userId == null)
            {
                return false;
            }

            await this.RemoveFromUserWantToReadListAsync(bookId, userId);
            await this.readListService.AddBookToUserBookListAsync(bookId, userId);

            return true;
        }

        public async Task<bool> IsBookInWantToReadListAsync(int bookId, string userId)
        {
            var userBook = await userBookRepository
                .FirstOrDefaultAsync(ub => ub.BookId == bookId && ub.ApplicationUserId == userId && ub.IsWantToRead);

            return userBook != null;
        }

        public async Task<bool> MoveBookToWantToReadListAsync(int bookId, string userId)
        {
            var userBook = await userBookRepository
                .FirstOrDefaultAsync(ub => ub.BookId == bookId && ub.ApplicationUserId == userId);

            if (userBook != null)
            {
                userBook.IsRead = false; 
                userBook.IsWantToRead = true; 
                await userBookRepository.UpdateAsync(userBook);
            }

            return true;
        }
    }
}
