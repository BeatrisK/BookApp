namespace BookApp.Services.Data.Interfaces
{
    using BookApp.Web.ViewModels.Author;
    public interface IAuthorService
    {
        Task<AuthorIndexViewModel> IndexGetAllBooksOfAuthorAsync(int id);
    }
}
