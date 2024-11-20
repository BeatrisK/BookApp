namespace BookApp.Web.ViewModels.Book
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Book;
    using static Common.EntityValidationConstants.Author;
    public class EditBookViewModel
    {
        [Required]
        public int Id { get; set; }

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

        public int AuthorId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string AuthorName { get; set; } = null!;
    }
}
