﻿namespace BookApp.Services.Data.Interfaces
{
    using BookApp.Web.ViewModels.Lists;
    using BookApp.Web.ViewModels.MyBook;

    public interface IWantToReadService
    {
        Task<bool> AddBookToUserWantToReadListAsync(int bookId, string userId);

        Task<IEnumerable<ApplicationUserListsViewModel>> GetUserWantToReadListByIdAsync(string id);

        Task<bool> RemoveFromUserWantToReadListAsync(int bookId, string userId);

        Task<bool> MarkBookAsReadAsync(int bookId, string userId);

        Task<bool> IsBookInWantToReadListAsync(int bookId, string userId);

        Task<bool> MoveBookToWantToReadListAsync(int bookId, string userId);
    }
}
