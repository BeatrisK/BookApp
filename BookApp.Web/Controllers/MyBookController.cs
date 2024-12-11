using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Book;
using BookApp.Web.ViewModels.MyBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Web.Controllers
{
    [Authorize]
    public class MyBookController : Controller
    {
        private readonly IMyBookService myBookService;
        public MyBookController(IMyBookService myBookService)
        {
            this.myBookService = myBookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexMyBookViewModel> books =
                await this.myBookService.IndexGetAllAsync();

            return View(books);
        }
    }
}
