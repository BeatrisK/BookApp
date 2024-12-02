namespace BookApp.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public int Pages { get; set; }

        public string Description { get; set; } = null!;

        public string Publisher { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<Review> Reviews { get; set; } 
            = new HashSet<Review>();
    }
}
