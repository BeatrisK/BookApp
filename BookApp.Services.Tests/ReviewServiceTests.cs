using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BookApp.Services.Data;
using BookApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using BookApp.Data.Models.Repository.Interfaces;
using BookApp.Web.ViewModels.Review;

[TestFixture]
public class ReviewServiceTests
{
    private Mock<IRepository<Review, int>> _mockReviewRepository;
    private Mock<IRepository<Book, int>> _mockBookRepository;
    private Mock<IRepository<ApplicationUser, string>> _mockUserRepository;
    private Mock<UserManager<ApplicationUser>> _mockUserManager;
    private ReviewService _reviewService;

    [SetUp]
    public void SetUp()
    {
        _mockReviewRepository = new Mock<IRepository<Review, int>>();
        _mockBookRepository = new Mock<IRepository<Book, int>>();
        _mockUserRepository = new Mock<IRepository<ApplicationUser, string>>();
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        _reviewService = new ReviewService(
            _mockReviewRepository.Object,
            _mockBookRepository.Object,
            _mockUserRepository.Object,
            _mockUserManager.Object);
    }

    [Test]
    public async Task IndexGetAllAsync_ShouldReturnReviews_WhenReviewsExist()
    {
        var reviews = new List<Review>
        {
            new Review { Id = 1, BookId = 1, Rating = 5, ReviewText = "Great book!", ReviewDate = DateTime.Now, UserId = "user1" },
            new Review { Id = 2, BookId = 1, Rating = 4, ReviewText = "Good book.", ReviewDate = DateTime.Now, UserId = "user2" }
        }.AsQueryable();

        _mockReviewRepository.Setup(r => r.GetAllAttached()).Returns(reviews);

        var books = new List<Book> { new Book { Id = 1, Title = "Test Book" } }.AsQueryable();
        _mockBookRepository.Setup(b => b.GetAllAttached()).Returns(books);

        var result = await _reviewService.IndexGetAllAsync(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.First().ReviewText, Is.EqualTo("Great book!"));
        Assert.That(result.First().Rating, Is.EqualTo(5));
    }

    [Test]
    public async Task AddReviewAsync_ShouldThrowException_WhenReviewAlreadyExists()
    {
        var existingReview = new Review { BookId = 1, UserId = "user1", IsDeleted = false };
        var model = new AddReviewViewModel
        {
            BookId = 1,
            UserId = "user1",
            Rating = 4,
            ReviewText = "Nice book",
            ReviewDate = DateTime.Now
        };

        _mockReviewRepository.Setup(r => r.GetAllAttached())
            .Returns(new List<Review> { existingReview }.AsQueryable());

        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _reviewService.AddReviewAsync(model));

        Assert.That(ex.Message, Is.EqualTo("You have already written a review for this book."));
    }

}