namespace BookApp.Web.Controllers
{
    using BookApp.Services.Data.Interfaces;
    using BookApp.Web.ViewModels.Author;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable <AuthorIndexViewModel> books =
                await this.authorService.IndexGetAllAsync();

            return View(books);
        }
    }
}
