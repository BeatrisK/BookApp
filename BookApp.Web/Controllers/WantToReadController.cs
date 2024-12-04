using BookApp.Data.Models;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Lists;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookApp.Web.Controllers
{
    public class WantToReadController : Controller
    {
        private readonly IWantToReadService wantToReadService;
        private readonly UserManager<ApplicationUser> userManager;

        public WantToReadController(IWantToReadService wantToReadService, UserManager<ApplicationUser> userManager)
        {
            this.wantToReadService = wantToReadService;
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
                await this.wantToReadService
                    .GetUserWantToReadListByIdAsync(userIdString); 

            return View(wantToReadList);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWantToRead(int bookId)
        {
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.wantToReadService
                .AddBookToUserWantToReadListAsync(bookId,  userId);

            if (result == false)
            {
                //TempData[nameof(AddToWatchlistNotSuccessfulMessage)] = AddToWatchlistNotSuccessfulMessage;
                return this.RedirectToAction("Index", "Movie");
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWantToRead(int bookId)
        {
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.wantToReadService
                .RemoveFromUserWantToReadListAsync(bookId, userId);

            if (result == false)
            {
                return this.RedirectToAction("Index", "Book");
            }

            return this.RedirectToAction(nameof(Index));
        }
    }
}
