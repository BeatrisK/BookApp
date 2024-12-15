namespace BookApp.Web.ViewModels.Book
{ 
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Book;
    using static Common.EntityValidationConstants.Author;
    using static Common.EntityValidationMessages.Book; 

    public class CreateBookViewModel
    {
        [Required(ErrorMessage = TitleRequiredMessage)]
        [MaxLength(TitleMaxLength)]
        [MinLength(TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MaxLength(GenreMaxLength)]
        [MinLength(GenreMinLength)]
        public string Genre { get; set; } = null!;

        [Required]
        public int Pages { get; set; }

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = PublisherRequiredMessage)]
        [MaxLength(PublisherMaxLength)]
        [MinLength(PublisherMinLength)]
        public string Publisher { get; set; } = null!;

        [Required(ErrorMessage = PriceRequiredMessage)]
        public decimal Price { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        [MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        public int? AuthorId { get; set; } 

        [Required(ErrorMessage = AuthorNameRequiredMessage)]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string AuthorName { get; set; } = null!;
    }
}
