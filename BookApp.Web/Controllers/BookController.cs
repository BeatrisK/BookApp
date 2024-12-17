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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3; // Колко книги да се показват на страница

            // Вземане на книгите за съответната страница
            var books = await this.bookService.GetBooksByPageAsync(page, pageSize);

            // Вземане на общия брой книги
            var totalBooks = await this.bookService.GetTotalBooksCountAsync();

            // Изчисляване на общия брой страници
            var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

            // Създаване на модел за изгледа
            var model = new BookIndexViewModel
            {
                Books = books,
                CurrentPage = page,
                TotalPages = totalPages
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
