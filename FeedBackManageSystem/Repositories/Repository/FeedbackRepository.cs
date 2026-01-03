using FeedBackManageSystem.Data;
using FeedBackManageSystem.Models;
using FeedBackManageSystem.Repositories.Interface;

namespace FeedBackManageSystem.Repositories.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<FeedbackViewModel> GetAll()
        {
            return _context.tblFeedback
                .Where(x => x.IsActive).ToList();
        }
        public FeedbackViewModel GetById(int id) 
        {
            return _context.tblFeedback.FirstOrDefault(x => x.Id == id);
        }
        public void Add(FeedbackViewModel model)
        {
            var entity = new FeedbackViewModel
            {
                Name = model.Name,
                MobileNumber = model.MobileNumber,
                Address = model.Address,
                ImagePath = model.ImagePath,
                Gender = model.Gender
            };
            _context.tblFeedback.Add(entity);
            _context.SaveChanges();
        }
        public void Update(FeedbackViewModel model) 
        {
            var entity = _context.tblFeedback.FirstOrDefault(x => x.Id == model.Id);
            if (entity == null)
                throw new Exception("Record Not found");

            entity.Name = model.Name;
            entity.MobileNumber = model.MobileNumber;
            entity.Gender = model.Gender;
            entity.ImagePath = model.ImagePath;
            entity.Address = model.Address;

            _context.SaveChanges();
        }
        public void Delete(int id) 
        {
            var entity = GetById(id);
            if (entity == null) return ;
           
            _context.tblFeedback.Remove(entity);
            _context.SaveChanges();
        }
    }
}
