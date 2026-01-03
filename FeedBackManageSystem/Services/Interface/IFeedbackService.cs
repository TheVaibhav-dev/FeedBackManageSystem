using FeedBackManageSystem.Models;

namespace FeedBackManageSystem.Services.Interface
{
    public interface IFeedbackService
    {
        IList<FeedbackViewModel> GetAll();
        FeedbackViewModel GetById(int id);
        bool Create(FeedbackViewModel feedback);
        bool Update(FeedbackViewModel model);
        void Delete(int id);
        //DataTableResult<FeedbackViewModel> SearchFeedbackdata(DataTableAjaxPostModel model, DateTime? startDate, DateTime? endDate);
    }
}
