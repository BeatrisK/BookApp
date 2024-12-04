using BookApp.Data.Models;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Lists;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Web.Controllers
{
    public class ReadListController : Controller
    {
        private readonly IReadListService readListService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReadListController(IReadListService readListService, UserManager<ApplicationUser> userManager)
        {
            this.readListService = readListService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userIdString = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userIdString))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            IEnumerable<ApplicationUserListsViewModel> wantToReadList =
                await this.readListService
                    .GetUserReadListByIdAsync(userIdString);

            return View(wantToReadList);
        }

        [HttpPost]
        public async Task<IActionResult> AddToReadList(int bookId)
        {
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.readListService
                .AddBookToUserBookListAsync(bookId, userId);

            if (result == false)
            {
                //TempData[nameof(AddToWatchlistNotSuccessfulMessage)] = AddToWatchlistNotSuccessfulMessage;
                return this.RedirectToAction("Index", "Movie");
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromReadList(int bookId)
        {
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.readListService
                .RemoveBookFromUserBookListAsync(bookId, userId);

            if (result == false)
            {
                return this.RedirectToAction("Index", "Book");
            }

            return this.RedirectToAction(nameof(Index));
        }
    }
}
