namespace BookApp.Web.Controllers
{
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Book;
    using Microsoft.AspNetCore.Mvc;

    public class BookController : Controller
    {
        private readonly IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
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
    }
}
