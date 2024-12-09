namespace BookApp.Web.ViewModels.Review
{
    using static BookApp.Common.EntityValidationConstants.Review;
    using System.ComponentModel.DataAnnotations;

    public class EditReviewViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(RatingMinValue, RatingMaxValue)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxLength)]
        [MinLength(ReviewTextMinLength)]
        public string ReviewText { get; set; } = null!;

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}
