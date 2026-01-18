using FeedBackManageSystem.Models;
using FeedBackManageSystem.Repositories.Interface;
using FeedBackManageSystem.Services.Interface;

namespace FeedBackManageSystem.Services.Service
{
    public class UserBlogService : IUserBlogService
    {
        private readonly IUserBlogRepository _repo;

        public UserBlogService(IUserBlogRepository repo)
        {
            _repo = repo;
        }

        public IQueryable<UserBlogViewModel> GetAll()
            => _repo.GetAll();

        public IList<UserBlogViewModel> GetById(long? id)
            => _repo.GetById(id);

        public bool Create(UserBlogViewModel model)
        {
            try
            {
                if (model != null)
                {
                    model.IsActive = true;
                    model.DateCreated = DateTime.Now;
                    _repo.Add(model);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(UserBlogViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var existing = _repo.GetById(model.Id);

                    if (existing == null)
                        throw new Exception("Record not found");


                    model.IsActive = true;
                    model.DateCreated = DateTime.Now;
                    _repo.Update(model);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        public void Delete(int id)
            => _repo.Delete(id);

        public Dictionary<int, int> GetMonthlyPairs(long userId)
        {
            return _repo.GetMonthlyPairs(userId);
        }


    }
}
