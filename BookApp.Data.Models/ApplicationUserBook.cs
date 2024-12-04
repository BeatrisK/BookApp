namespace BookApp.Data.Models
{
    public class ApplicationUserBook
    {
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public int BookId { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
