using FeedBackManageSystem.Models;
using System.Security.Cryptography;

namespace FeedBackManageSystem.Services.Interface
{
    public interface IUserBlogService
    {
        IQueryable<UserBlogViewModel> GetAll();
        IList<UserBlogViewModel> GetById(long? id);
        bool Create(UserBlogViewModel model);
        bool Update(UserBlogViewModel model);
        void Delete(int id);
        Dictionary<int, int> GetMonthlyPairs(long userId);
    }
}
