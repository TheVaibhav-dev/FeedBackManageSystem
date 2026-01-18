using FeedBackManageSystem.Data;
using FeedBackManageSystem.Models;
using FeedBackManageSystem.Repositories.Interface;
namespace FeedBackManageSystem.Repositories.Repository
{
    public class UserBlogRepository : IUserBlogRepository
    {
        private readonly ApplicationDbContext _context;
        public UserBlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<UserBlogViewModel> GetAll()
        {
            return _context.tblUserBlog.Where(x=>x.IsActive== true);
        }
        public IList<UserBlogViewModel> GetById(long? id)
        {
            return _context.tblUserBlog.Where(x=>x.IdUser == id).ToList();
        }
        public UserBlogViewModel GetId(long id)
        {
            return _context.tblUserBlog.Where(x=>x.Id == id).FirstOrDefault();
        }
        public void Add(UserBlogViewModel user)
        {
            _context.tblUserBlog.Add(user);
            _context.SaveChanges();
        }

        public void Update(UserBlogViewModel user)
        {
            var entity = GetId(user.Id);
            if (entity == null)
                throw new Exception("Record Not Found");

            _context.tblUserBlog.Update(user);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var user = GetId(id);
            if (user == null) return;

            user.IsActive = false;
            _context.SaveChanges();
        }
        public Dictionary<int, int> GetMonthlyPairs(long userId)
        {
            return _context.tblUserBlog.Where(x => x.IdUser == userId && x.IsActive == true).GroupBy(x => x.DateCreated.Month).Select(x => new
            {
                Month = x.Key,
                Count = x.Count()
            })
                .ToDictionary(x=>x.Month,x=>x.Count);
        }
    }
}
