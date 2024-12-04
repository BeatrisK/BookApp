namespace BookApp.Services.Data.Interfaces
{
    using BookApp.Web.ViewModels.Book;
    using Web.ViewModels.MyBook;
    public interface IMyBookService
    {
        Task<IEnumerable<IndexMyBookViewModel>> IndexGetAllAsync();
    }
}
