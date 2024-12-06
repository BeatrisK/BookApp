namespace BookApp.Web.Controllers
{
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Book;
	using Microsoft.AspNetCore.Mvc;

    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IReviewService reviewService;
        public BookController(IBookService bookService, IReviewService reviewService)
        {
            this.bookService = bookService;
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookIndexViewModel> books =
                await this.bookService.IndexGetAllAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.bookService.CreateBookAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            BookDetailsViewModel? book = await this.bookService
                .GetBookDetailsByIdAsync(id);

            if (book == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditBookViewModel? formModel = await this.bookService
                .GetBookForEditByIdAsync(id);

            if (formModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(formModel);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(EditBookViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return this.View(model);
			}

			bool isUpdated = await this.bookService
				.EditBookAsync(model);

			if (!isUpdated)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating the cinema! Please contact administrator");
				return this.View(model);
			}

			return this.RedirectToAction(nameof(Details), "Book", new { id = model.Id });
		}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        { 
            DeleteBookViewModel? bookToDeleteViewModel =
                await this.bookService.GetBookForDeleteByIdAsync(id);

            if (bookToDeleteViewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(bookToDeleteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteConfirmed(DeleteBookViewModel book)
        {
            bool isDeleted = await this.bookService
                .SoftDeleteBookAsync(book.Id);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] =
                    "Unexpected error occurred while trying to delete the book! Please contact system administrator!";
                return this.RedirectToAction(nameof(Delete), new { id = book.Id });
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Reviews()
        {                
           var reviews = this.reviewService.IndexGetAllAsync();

            return View(reviews);
        }
    }
}
