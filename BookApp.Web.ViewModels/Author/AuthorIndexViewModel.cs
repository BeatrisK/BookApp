namespace BookApp.Web.ViewModels.Author
{
    using BookApp.Data.Models;
    using BookApp.Web.ViewModels.Book;

    public class AuthorIndexViewModel
    {
        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public IEnumerable<BookIndexViewModel> Books { get; set; } 
            = new List<BookIndexViewModel>();
    }
}
