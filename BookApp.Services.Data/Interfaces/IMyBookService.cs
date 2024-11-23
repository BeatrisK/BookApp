namespace BookApp.Services.Data.Interfaces
{
    using BookApp.Web.ViewModels.Book;
    using Web.ViewModels.MyBook;
    public interface IMyBookService
    {
        Task<IEnumerable<IndexMyBookViewModel>> IndexGetAllAsync();

        Task<IEnumerable<UserReadBooksViewModel>> GetUserBookListByIdAsync(int id);

        Task<bool> AddBookToUserBookListAsync(string? bookId, string userId);

        Task<bool> RemoveBookFromUserBookAsync(string? bookId, string userId);

        Task<IEnumerable<UserReadBooksViewModel>> GetUserWantToReadListkByIdAsync(int id);

        Task<bool> AddUserWantToReadListAsync(string? bookId, string userId);

        Task<bool> RemoveFromUserWantToReadListAsync(string? bookId, string userId);
    }
}
