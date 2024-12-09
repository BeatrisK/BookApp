using BookApp.Web.ViewModels.Review;

namespace BookApp.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewIndexViewModel>> IndexGetAllAsync(int bookId);

        Task AddReviewAsync(AddReviewViewModel model);

        Task<EditReviewViewModel> GetReviewForEditByIdAsync(int id);

        Task<bool> EditReviewAsync(EditReviewViewModel model);

        Task<DeleteReviewViewModel?> GetReviewForDeleteByIdAsync(int id);

        Task<bool> SoftDeleteReviewAsync(int id);

        Task<bool> UserHasWrittenReviewForBookAsync(int bookId, string userId);
    }
}
