namespace BookApp.Web.ViewModels.Review
{
    using BookApp.Data.Models;
    using System.Security.Principal;

    public class ReviewIndexViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int Rating { get; set; }

        public string ReviewText { get; set; } = null!;

        public DateTime ReviewDate { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
