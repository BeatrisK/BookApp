namespace BookApp.Web.ViewModels.Book
{ 
    using System.ComponentModel.DataAnnotations;
    using static BookApp.Common.EntityValidationConstants.Book;
    using static BookApp.Common.EntityValidationConstants.Author;
    public class CreateBookViewModel
    {
        [Required]
        [MaxLength(TitleMaxLength)]
        [MinLength(TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GenreMaxLength)]
        [MinLength(GenreMinLength)]
        public string Genre { get; set; } = null!;

        [Required]
        public int Pages { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(PublisherMaxLength)]
        [MinLength(PublisherMinLength)]
        public string Publisher { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        [MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string AuthorName { get; set; } = null!;
    }
}
