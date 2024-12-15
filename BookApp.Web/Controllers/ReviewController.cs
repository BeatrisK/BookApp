namespace BookApp.Web.Controllers
{
    using BookApp.Data.Models;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IReadListService readListService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(IReviewService reviewService, IReadListService readListService, 
            UserManager<ApplicationUser> userManager)
        {
            this.reviewService = reviewService;
            this.readListService = readListService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int bookId)
        {
            var userId = this.userManager.GetUserId(User);
            ViewData["UserId"] = userId;

            IEnumerable<ReviewIndexViewModel> reviews =
                await this.reviewService.IndexGetAllAsync(bookId);

            return View(reviews);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add(int bookId)
        {
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var existingReview = await reviewService.UserHasWrittenReviewForBookAsync(bookId, userId);

            if (existingReview)
            {
                TempData["ErrorMessage"] = "You have already written a review for this book.";
                return RedirectToAction("Index"); 
            }

            var model = new AddReviewViewModel
            {
                BookId = bookId,
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            var isInReadList = await this.readListService
                .IsBookInReadListAsync(model.BookId, userId);

            if (!isInReadList)
            {
                return Unauthorized("You can only review books from your ReadList.");
            }

            await this.reviewService.AddReviewAsync(model);

            TempData["SuccessMessage"] = "Review successfully updated!";
            return this.RedirectToAction(nameof(Index), "Review", new { bookId = model.BookId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            EditReviewViewModel? formModel = await this.reviewService
                .GetReviewForEditByIdAsync(id);

            if (formModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            string currentUserId = this.userManager.GetUserId(User);

            if (formModel.UserId != currentUserId)
            { 
                return Unauthorized("You are not authorized to edit this review.");
            }

            return this.View(formModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            string currentUserId = this.userManager.GetUserId(User);
            if (model.UserId != currentUserId)
            {
                ModelState.AddModelError(string.Empty, "You are not authorized to edit this review.");
                return this.View(model);
            }

            bool isUpdated = await this.reviewService.EditReviewAsync(model);

            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating the review! Please contact administrator");
                return this.View(model);
            }

            TempData["SuccessMessage"] = "Review successfully updated!";
            return this.RedirectToAction(nameof(Index), "Review", new { bookId = model.BookId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteReviewViewModel? reviewToDeleteViewModel =
           await this.reviewService.GetReviewForDeleteByIdAsync(id);

            if (reviewToDeleteViewModel == null)
            {
                return this.RedirectToAction(nameof(Index), "Review", new { bookId = id });
            }

            return this.View(reviewToDeleteViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SoftDeleteConfirmed(DeleteReviewViewModel review)
        {
            bool isDeleted = await this.reviewService
                .SoftDeleteReviewAsync(review.Id);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] =
                    "Unexpected error occurred while trying to delete the review! Please contact system administrator!";
                return this.RedirectToAction(nameof(Delete), new { id = review.Id });
            }

            TempData["SuccessMessage"] = "Review successfully deleted!";
            return this.RedirectToAction(nameof(Index), "Review", new { bookId = review.BookId });
        }
    }
}
