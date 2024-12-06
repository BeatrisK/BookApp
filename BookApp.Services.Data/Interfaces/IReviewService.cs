using BookApp.Web.ViewModels.Review;

namespace BookApp.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewIndexViewModel>> IndexGetAllAsync();

        Task AddReviewAsync(AddReviewViewModel model);

        Task<EditReviewViewModel> GetReviewForEditByIdAsync(int id);

        public Task<bool> EditReviewAsync(EditReviewViewModel model);

        public Task<DeleteReviewViewModel?> GetReviewForDeleteByIdAsync(int id);

        public Task<bool> SoftDeleteReviewAsync(int id);
    }
}
