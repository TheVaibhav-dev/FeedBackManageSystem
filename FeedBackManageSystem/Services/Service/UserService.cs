using FeedBackManageSystem.Models;
using FeedBackManageSystem.Repositories.Interface;
using FeedBackManageSystem.Services.Interface;
using System.Security.Cryptography;
using System.Text;

namespace FeedBackManageSystem.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly PasswordService _passwordService;
        public UserService(IUserRepository repo,PasswordService passwordService)
        {
           _repo = repo;
            _passwordService = passwordService;
        }
        public IQueryable<UserViewModel> GetAll() => _repo.GetAll();
        public UserViewModel GetUser(long? id) => _repo.GetById(id);
        public bool Create(UserViewModel model)
        {
            try
            {
                if (model != null)
                {
                    model.PasswordHash = _passwordService.Hash(model.PasswordHash);
                    model.IsActive = true;
                    model.DateCreated = DateTime.Now;

                    _repo.Add(model);
                    return true;
                }
                return false;
            }
            catch {
                return false;
            }
        }
        public UserViewModel Authenticate(string email, string password)
        {
            var user = _repo.GetByEmail(email);
            if (user == null)
                return null;
            bool isValidPassword = _passwordService.Verify(user.PasswordHash,password);
                if (!isValidPassword)
                return null;
            return user;
        }
    }
}
