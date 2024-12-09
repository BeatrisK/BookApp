namespace BookApp.Services.Data
{
    using BookApp.Data.Models;
    using BookApp.Data.Models.Repository.Interfaces;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Net.Http;
    using System.Security.Claims;

    public class ReviewService : IReviewService
    {
        private IRepository<Review, int> reviewRepository;
        private IRepository<Book, int> bookRepository;
        private IRepository<ApplicationUser, string> applicationUserRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewService(IRepository<Review, int> reviewRepository, IRepository<Book, int> bookRepository,
            IRepository<ApplicationUser, string> applicationUserRepository, UserManager<ApplicationUser> userManager)
        {
            this.reviewRepository = reviewRepository;
            this.bookRepository = bookRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ReviewIndexViewModel>> IndexGetAllAsync(int bookId)
        {
            IEnumerable<ReviewIndexViewModel> review = await this.reviewRepository
                .GetAllAttached()
                .Include(r => r.Book)
                .Where(r => r.IsDeleted == false && r.BookId == bookId)
                .Select(r => new ReviewIndexViewModel
                {
                    Id = r.Id,
                    BookId = r.Book.Id,
                    Book = r.Book,
                    Rating = r.Rating,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate,
                    UserId = r.UserId
                })
                .ToArrayAsync();

            return review;
        }

        public async Task AddReviewAsync(AddReviewViewModel model)
        {
            Book? book = await bookRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(b => b.Id == model.BookId);

            if (book == null)
            {
                throw new InvalidOperationException($"Book with ID {model.BookId} was not found.");
            }

            var existingReview = await this.reviewRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(r => r.BookId == model.BookId && r.UserId == model.UserId && r.IsDeleted == false);

            if (existingReview != null)
            {
                throw new InvalidOperationException("You have already written a review for this book.");
            }

            Review review = new Review
            {
                Id = model.Id,
                BookId = book.Id,
                Rating = model.Rating,
                ReviewText = model.ReviewText,
                ReviewDate = model.ReviewDate,
                UserId = model.UserId
            };

            await this.reviewRepository.AddAsync(review);
        }

        public async Task<EditReviewViewModel> GetReviewForEditByIdAsync(int id) 
        {
            Review? review = await this.reviewRepository
               .GetAllAttached()
               .Include(r => r.Book)
               .Where(r => r.IsDeleted == false)
               .FirstOrDefaultAsync(r => r.Id == id);

            var reviewModel = new EditReviewViewModel()
            {
                Id = review.Id,
                BookId = review.BookId,
                Rating = review.Rating,
                ReviewText = review.ReviewText ?? string.Empty,
                ReviewDate = review.ReviewDate,
                UserId = review.UserId ?? string.Empty
            };

            return reviewModel;
        }

        public async Task<bool> EditReviewAsync(EditReviewViewModel model)
        {
            var review = await reviewRepository
           .GetAllAttached()
           .FirstOrDefaultAsync(r => r.Id == model.Id && !r.IsDeleted);

            if (review == null)
            {
                return false;
            }

            if (review.UserId != model.UserId)
            {
                throw new UnauthorizedAccessException("You do not have permission to edit this review.");
            }

            review.Rating = model.Rating;
            review.ReviewText = model.ReviewText;
            review.ReviewDate = model.ReviewDate;

            return await reviewRepository.UpdateAsync(review);
        }

         public async Task<DeleteReviewViewModel?> GetReviewForDeleteByIdAsync(int id)
        {
            DeleteReviewViewModel? review = await this.reviewRepository
                .GetAllAttached()
                .Where(c => c.IsDeleted == false)
                .Select(b => new DeleteReviewViewModel()
                {
                    Id = b.Id,
                    Book = b.Book,
                })
                .FirstOrDefaultAsync(b => b.Id == id);

            return review;
        }

        public async Task<bool> SoftDeleteReviewAsync(int id)
        {
            Review? reviewToDelete = await this.reviewRepository
                .FirstOrDefaultAsync(c => c.Id == id);

            if (reviewToDelete == null)
            {
                return false;
            }

            reviewToDelete.IsDeleted = true;
            return await this.reviewRepository.UpdateAsync(reviewToDelete);
        }

        public async Task<bool> UserHasWrittenReviewForBookAsync(int bookId, string userId)
        {
            var user = await applicationUserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var review = await reviewRepository
                .FirstOrDefaultAsync(r => r.BookId == bookId && r.UserId == userId && !r.IsDeleted);

            return review != null;
        }
    }
}
