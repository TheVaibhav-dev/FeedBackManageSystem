using FeedBackManageSystem.Models;

namespace FeedBackManageSystem.Repositories.Interface
{
    public interface IUserBlogRepository
    {
        IQueryable<UserBlogViewModel> GetAll();
        IList<UserBlogViewModel> GetById(long? id);
        void Add(UserBlogViewModel user);
        void Update(UserBlogViewModel user);
        void Delete(long id);
        Dictionary<int, int> GetMonthlyPairs(long userId);
    }
}
