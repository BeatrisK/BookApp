namespace BookApp.Web.ViewModels.Review
{
    using BookApp.Data.Models;

    public class ReviewIndexViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;
        public string BookAuthor { get; set; } = null!;
        public Book Book { get; set; } = null!;

        public int Rating { get; set; }

        public string ReviewText { get; set; } = null!;

        public DateTime ReviewDate { get; set; }
    }
}
