﻿namespace BookApp.Data.Models
{
    public class ReadList
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int Rating { get; set; }
    }
}