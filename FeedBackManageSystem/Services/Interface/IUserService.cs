using FeedBackManageSystem.Models;

namespace FeedBackManageSystem.Services.Interface
{
    public interface IUserService
    {
        IQueryable<UserViewModel> GetAll();
        UserViewModel GetUser(long? id);
        bool Create(UserViewModel model);
        UserViewModel Authenticate(string email, string password);
    }
}
