namespace BookApp.Services.Data
{
    using BookApp.Data.Models;
    using BookApp.Data.Models.Repository.Interfaces;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Book;
    using BookApp.Web.ViewModels.Review;
    using Microsoft.EntityFrameworkCore;

    public class ReviewService : IReviewService
    {
        private IRepository<Review, int> reviewRepository;
        private IRepository<Book, int> bookRepository;

        public ReviewService(IRepository<Review, int> reviewRepository, IRepository<Book, int> bookRepository)
        {
            this.reviewRepository = reviewRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<ReviewIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<ReviewIndexViewModel> review = await this.reviewRepository
                .GetAllAttached()
                .Include(r => r.Book)
                .Where(r => r.IsDeleted == false)
                .Select(r => new ReviewIndexViewModel
                {
                    BookId = r.Book.Id,
                    BookTitle = r.Book.Title,
                    BookAuthor = r.Book.Author.Name,
                    Book = r.Book,
                    Rating = r.Rating,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate,
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

            Review review = new Review
            {
                Id = model.Id,
                BookId = book.Id,
                Rating = model.Rating,
                ReviewText = model.ReviewText,
                ReviewDate = model.ReviewDate
            };

            await this.reviewRepository.AddAsync(review);
        }

        public Task<EditReviewViewModel> GetReviewForEditByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditReviewAsync(EditReviewViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteReviewViewModel?> GetReviewForDeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteReviewAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
