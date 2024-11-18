﻿namespace BookApp.Services.Data.Interfaces
{
    using Web.ViewModels.Book;
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync();

        Task CreateBookAsync(CreateBookViewModel model);

        Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int id);
    }
}
