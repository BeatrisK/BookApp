namespace BookApp.Web.Controllers
{
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Book;
    using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 3;

            IEnumerable<BookIndexViewModel> books;
            int totalBooks;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = await this.bookService.SearchBooksAsync(searchString, page, pageSize);
                totalBooks = await this.bookService.GetSearchBooksCountAsync(searchString);
            }
            else
            {
                books = await this.bookService.GetBooksByPageAsync(page, pageSize);
                totalBooks = await this.bookService.GetTotalBooksCountAsync();
            }

            int totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

            // Добавяне на SearchString във ViewData
            ViewData["SearchString"] = searchString;

            var model = new BookIndexViewModel
            {
                Books = books,
                CurrentPage = page,
                TotalPages = totalPages,
                SearchString = searchString
            };

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        public IActionResult Reviews(int bookId)
        {                
           var reviews = this.reviewService.IndexGetAllAsync(bookId);

            return View(reviews);
        }
    }
}
