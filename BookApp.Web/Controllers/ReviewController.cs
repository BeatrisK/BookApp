namespace BookApp.Web.Controllers
{
    using BookApp.Data.Models;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            var userId = this.userManager.GetUserId(User);
            ViewData["UserId"] = userId;

            IEnumerable<ReviewIndexViewModel> books =
                await this.reviewService.IndexGetAllAsync();

            return View(books);
        }

        [HttpGet]
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
                return RedirectToAction("Index", "MyBook"); 
            }

            var model = new AddReviewViewModel
            {
                BookId = bookId,
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
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

            bool hasWrittenReview = await this.reviewService
                .UserHasWrittenReviewForBookAsync(model.BookId, userId);

            if (hasWrittenReview)
            {
                TempData["ErrorMessage"] = "You have already written a review for this book.";
                return RedirectToAction(nameof(Index));
            }

            await this.reviewService.AddReviewAsync(model);

            TempData["SuccessMessage"] = "Review successfully updated!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditReviewViewModel? formModel = await this.reviewService
                .GetReviewForEditByIdAsync(id);

            if (formModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            string userId =  this.userManager.GetUserId(User);
            formModel.UserId = userId;

            return this.View(formModel);
        }

        [HttpPost]
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

            return this.RedirectToAction(nameof(Index), "Review", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteReviewViewModel? reviewToDeleteViewModel =
           await this.reviewService.GetReviewForDeleteByIdAsync(id);

            if (reviewToDeleteViewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(reviewToDeleteViewModel);
        }

        [HttpPost]
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

            return this.RedirectToAction(nameof(Index));
        }
    }
}
