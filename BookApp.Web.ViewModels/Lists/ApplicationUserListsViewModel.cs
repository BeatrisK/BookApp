namespace BookApp.Web.ViewModels.Lists
{
    public class ApplicationUserListsViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public int AuthorId{ get; set; }

        public string AuthorName { get; set; } = null!;

        public string? ImageUrl { get; set; } = null!;
    }
}
