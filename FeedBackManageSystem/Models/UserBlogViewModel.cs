namespace FeedBackManageSystem.Models
{
    public class UserBlogViewModel
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public int UserExperience { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
