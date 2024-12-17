namespace BookApp.Web.ViewModels.Book
{
    public class BookIndexViewModel 
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int AuthorId { get; set; } 
        public string AuthorName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public IEnumerable<BookIndexViewModel> Books { get; set; } 
            = new List<BookIndexViewModel>(); 
        public int CurrentPage { get; set; } 
        public int TotalPages { get; set; } 
    }
}
