namespace BookApp.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; } = null!; //TODO relations

        public int Rating { get; set; }

        public string ReviewText { get; set; } = null!;

        public DateTime ReviewDate { get; set; }
    }
}
