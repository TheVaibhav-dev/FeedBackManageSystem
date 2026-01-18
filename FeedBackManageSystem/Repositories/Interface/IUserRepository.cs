using FeedBackManageSystem.Models;

namespace FeedBackManageSystem.Repositories.Interface
{
    public interface IUserRepository
    {
        IQueryable<UserViewModel> GetAll();
        UserViewModel GetById(long? id);
        UserViewModel GetByEmail(string email);
        void Add(UserViewModel user);
        void Update(UserViewModel user); 
        void Delete(long id);
    }
}
