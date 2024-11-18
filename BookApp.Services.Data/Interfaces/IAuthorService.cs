namespace BookApp.Services.Data.Interfaces
{
    using BookApp.Web.ViewModels.Author;
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorIndexViewModel>> IndexGetAllAsync();
    }
}
