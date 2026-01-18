using Microsoft.EntityFrameworkCore;
using FeedBackManageSystem.Models;

namespace FeedBackManageSystem.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<FeedbackViewModel> tblFeedback { get; set; }
        public DbSet<UserBlogViewModel> tblUserBlog { get; set; }
        public DbSet<UserViewModel> tblUser { get; set; }
    }
}
