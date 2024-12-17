namespace BookApp.Services.Data.Interfaces
{
    using Web.ViewModels.Book;
    public interface IBookService
    {
        public Task<int> GetTotalBooksCountAsync();

        public Task<IEnumerable<BookIndexViewModel>> GetBooksByPageAsync(int page, int pageSize);

        public Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync();

        public Task CreateBookAsync(CreateBookViewModel model);

        public Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int id);

        public Task<EditBookViewModel> GetBookForEditByIdAsync(int id);

        public Task<bool> EditBookAsync(EditBookViewModel model);

        public Task<DeleteBookViewModel?> GetBookForDeleteByIdAsync(int id);

        public Task<bool> SoftDeleteBookAsync(int id);

        public Task<IEnumerable<BookIndexViewModel>> SearchBooksAsync(string searchString, int page, int pageSize);

        public Task<int> GetSearchBooksCountAsync(string searchString);
    }
}
