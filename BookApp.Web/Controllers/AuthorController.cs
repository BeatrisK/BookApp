namespace BookApp.Web.Controllers
{
    using BookApp.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var viewModel = await authorService.IndexGetAllBooksOfAuthorAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}
