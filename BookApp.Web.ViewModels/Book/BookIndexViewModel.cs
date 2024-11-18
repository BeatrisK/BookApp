namespace BookApp.Web.ViewModels.Book
{
    public class BookIndexViewModel 
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
