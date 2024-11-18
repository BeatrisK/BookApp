﻿namespace BookApp.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public virtual ICollection<Book> Books { get; set; } =
            new HashSet<Book>();
    }
}