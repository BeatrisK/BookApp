using BookApp.Services.Data;
using BookApp.Data.Models;
using BookApp.Web.ViewModels.Review;
using BookApp.Data.Models.Repository;
using BookApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private BookDbContext context;
        private ReviewService reviewService;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: $"BookDb_{Guid.NewGuid()}")
                .Options;

            context = new BookDbContext(options);

            var user = new ApplicationUser { Id = "user1", UserName = "testuser" };
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Fiction",
                Pages = 300,
                Description = "Test Description",
                Publisher = "Test Publisher",
                Price = 19.99M,
                IsDeleted = false
            };
            var review = new Review
            {
                Id = 1,
                BookId = 1,
                Rating = 5,
                ReviewText = "Great book!",
                ReviewDate = DateTime.UtcNow,
                UserId = "user1",
                IsDeleted = false
            };

            context.Users.Add(user);
            context.Books.Add(book);
            context.Reviews.Add(review);
            await context.SaveChangesAsync();

            var reviewRepo = new BaseRepository<Review, int>(context);
            var bookRepo = new BaseRepository<Book, int>(context);
            var userRepo = new BaseRepository<ApplicationUser, string>(context);

            reviewService = new ReviewService(reviewRepo, bookRepo, userRepo, null);
        }

        [TearDown]
        public async Task Teardown()
        {
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.DisposeAsync();
            }
        }

        [Test]
        public async Task IndexGetAllAsync_ShouldReturnReviewsForGivenBookId()
        {
            var reviews = await reviewService.IndexGetAllAsync(1);

            Assert.That(reviews.Count(), Is.EqualTo(1), "Expected 1 review for the book.");
            Assert.That(reviews.First().ReviewText, Is.EqualTo("Great book!"), "Review text does not match.");
        }

        [Test]
        public void AddReviewAsync_ShouldThrowException_WhenBookNotFound()
        {
            var model = new AddReviewViewModel
            {
                BookId = 999, 
                Rating = 4,
                ReviewText = "New review",
                UserId = "user1",
                ReviewDate = DateTime.UtcNow
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await reviewService.AddReviewAsync(model));
        }

        [Test]
        public async Task AddReviewAsync_ShouldAddReview_WhenValidModelIsProvided()
        {
            var model = new AddReviewViewModel
            {
                BookId = 1,
                Rating = 4,
                ReviewText = "Another review",
                UserId = "newUser", 
                ReviewDate = DateTime.UtcNow
            };

            await reviewService.AddReviewAsync(model);

            var reviews = await context.Reviews.Where(r => r.BookId == 1).ToListAsync();
            Assert.That(reviews.Count, Is.EqualTo(2), "Expected 2 reviews for the book.");
            Assert.That(reviews.Any(r => r.ReviewText == "Another review"), Is.True, "The new review was not added.");
        }

        [Test]
        public async Task EditReviewAsync_ShouldUpdateReview_WhenValidModelIsProvided()
        {
            var model = new EditReviewViewModel
            {
                Id = 1,
                BookId = 1,
                Rating = 3,
                ReviewText = "Updated review",
                ReviewDate = DateTime.UtcNow,
                UserId = "user1"
            };

            var result = await reviewService.EditReviewAsync(model);

            Assert.That(result, Is.True, "EditReviewAsync should return true on successful update.");
            var updatedReview = await context.Reviews.FirstAsync(r => r.Id == 1);
            Assert.That(updatedReview.ReviewText, Is.EqualTo("Updated review"), "Review text was not updated.");
        }

        [Test]
        public async Task SoftDeleteReviewAsync_ShouldMarkReviewAsDeleted()
        {
            var result = await reviewService.SoftDeleteReviewAsync(1);

            Assert.That(result, Is.True, "SoftDeleteReviewAsync should return true on successful deletion.");
            var deletedReview = await context.Reviews.FirstAsync(r => r.Id == 1);
            Assert.That(deletedReview.IsDeleted, Is.True, "Review was not marked as deleted.");
        }

        [Test]
        public async Task UserHasWrittenReviewForBookAsync_ShouldReturnTrue_WhenUserHasReview()
        {
            var result = await reviewService.UserHasWrittenReviewForBookAsync(1, "user1");

            Assert.That(result, Is.True, "Expected true for existing review by the user.");
        }

        [Test]
        public async Task UserHasWrittenReviewForBookAsync_ShouldReturnFalse_WhenUserHasNoReview()
        {
            var result = await reviewService.UserHasWrittenReviewForBookAsync(1, "user2");

            Assert.That(result, Is.False, "Expected false for user with no reviews.");
        }
    }
}