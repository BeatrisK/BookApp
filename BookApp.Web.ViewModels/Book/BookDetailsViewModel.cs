namespace BookApp.Web.ViewModels.Book
{
    using System.ComponentModel.DataAnnotations;
    using static BookApp.Common.EntityValidationConstants.Book;
    using static BookApp.Common.EntityValidationConstants.Author;
    public class BookDetailsViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [MinLength(TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GenreMaxLength)]
        [MinLength(GenreMinLength)]
        public string Genre { get; set; } = null!;

        public int Pages { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(PublisherMaxLength)]
        [MinLength(PublisherMinLength)]
        public string Publisher { get; set; } = null!;

        public decimal Price { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        [MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Author { get; set; } = null!;
    }
}
