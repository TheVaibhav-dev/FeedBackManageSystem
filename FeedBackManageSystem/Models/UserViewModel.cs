using FeedBackManageSystem.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FeedBackManageSystem.Models
{
    public class UserViewModel
    {
        public long Id{ get; set; }
        [DisplayName("Full Name")]
        [Required(ErrorMessage ="Name is required")]
        public  string FullName {get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage ="Email is required")]
        public string Emial { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage ="Password is required")]
        public string PasswordHash { get; set; }
        [DisplayName("User Type")]
        [Required(ErrorMessage ="Select any one")]
        public UserType UserType{ get; set; }
        public bool IsActive{ get; set; }
        public DateTime DateCreated{ get; set; } = DateTime.Now;
    }
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
