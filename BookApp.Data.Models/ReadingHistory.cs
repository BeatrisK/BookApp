namespace BookApp.Data.Models
{
    public class ReadingHistory
    {
        //public int UserId { get; set; }

        //TODO public User User { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string Status { get; set; } = null!;
    }
}
