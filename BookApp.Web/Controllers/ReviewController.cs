namespace BookApp.Web.Controllers
{
    using BookApp.Data.Models;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Book;
    using BookApp.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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
            IEnumerable<ReviewIndexViewModel> books =
                await this.reviewService.IndexGetAllAsync();

            return View(books);
        }

        [HttpGet]
        public IActionResult Add(int bookId)
        {
            var model = new AddReviewViewModel
            {
                BookId = bookId,
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

            var isInReadList = await this.readListService.IsBookInReadListAsync(model.BookId, userId);

            if (!isInReadList)
            {
                return Unauthorized("You can only review books from your ReadList.");
            }

            await this.reviewService.AddReviewAsync(model);

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

            return this.View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            bool isUpdated = await this.reviewService
                .EditReviewAsync(model);

            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating the review! Please contact administrator");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(Index), "Review", new { id = model.Id });
        }
    }
}
