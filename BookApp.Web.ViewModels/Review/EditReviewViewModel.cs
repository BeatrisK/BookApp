namespace BookApp.Web.ViewModels.Review
{
    using BookApp.Data.Models;
    using static BookApp.Common.EntityValidationConstants.Review;
    using System.ComponentModel.DataAnnotations;

    public class EditReviewViewModel
    {
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [Required]
        [MaxLength(RatingMaxValue)]
        [MinLength(RatingMinValue)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxLength)]
        [MinLength(ReviewTextMinLength)]
        public string ReviewText { get; set; } = null!;

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}
