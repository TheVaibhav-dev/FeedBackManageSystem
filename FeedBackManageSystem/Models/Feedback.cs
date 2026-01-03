using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedBackManageSystem.Models
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Mobile Number is Required")]
        [RegularExpression(@"^[6-9]\d{9}$",ErrorMessage ="Enter valid 10-digit mobile number")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage ="Enter your address")]
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        public string Gender { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
