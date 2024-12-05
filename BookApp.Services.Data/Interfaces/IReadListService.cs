using BookApp.Web.ViewModels.Lists;
using BookApp.Web.ViewModels.MyBook;

namespace BookApp.Services.Data.Interfaces
{
    public interface IReadListService
    {
        Task<IEnumerable<ApplicationUserListsViewModel>> GetUserReadListByIdAsync(string id);

        Task<bool> AddBookToUserBookListAsync(int bookId, string userId);

        Task<bool> RemoveBookFromUserBookListAsync(int bookId, string userId);

        Task<bool> IsBookInReadListAsync(int bookId, string userId);
        
    }
}
