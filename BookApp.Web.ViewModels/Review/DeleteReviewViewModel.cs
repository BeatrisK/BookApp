namespace BookApp.Web.ViewModels.Review
{
    using BookApp.Data.Models;

    public class DeleteReviewViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
