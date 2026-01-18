using FeedBackManageSystem.Data;
using FeedBackManageSystem.Models;
using FeedBackManageSystem.Repositories.Interface;

namespace FeedBackManageSystem.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<UserViewModel> GetAll()
        {
            return _context.tblUser.Where(x=>x.IsActive == true);
        }
        public UserViewModel GetById(long? id)
        {
            return _context.tblUser.FirstOrDefault(x => x.Id == id);
        }
        public UserViewModel GetByEmail(string email)
        {
            return _context.tblUser.FirstOrDefault(x => x.Emial == email);
        }
        public void Add(UserViewModel user)
        {
            var entity = GetByEmail(user.Emial);
            if (entity != null) 
                throw new Exception("Record Already Exists");

            _context.tblUser.Add(user);
            _context.SaveChanges();
        }

        public void Update(UserViewModel user)
        {
            var entity = GetById(user.Id);
            if(entity == null) 
                throw new Exception("Record Not Found");

            _context.tblUser.Update(user);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var user = GetById(id);
            if (user == null) return;

            user.IsActive = false;   
            _context.SaveChanges();
        }
    }
}
