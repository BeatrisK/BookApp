namespace BookApp.Web.Controllers
{
    using BookApp.Data.Models;
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Review;
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
    }
}
