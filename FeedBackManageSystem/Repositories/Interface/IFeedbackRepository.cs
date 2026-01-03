using FeedBackManageSystem.Models;

namespace FeedBackManageSystem.Repositories.Interface
{
    public interface IFeedbackRepository
    {
        IList<FeedbackViewModel> GetAll();
        FeedbackViewModel GetById(int id);
        void Add(FeedbackViewModel model);
        void Update(FeedbackViewModel model);
        void Delete(int id);
    }
}
