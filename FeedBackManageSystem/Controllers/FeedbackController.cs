using FeedBackManageSystem.Data;
using FeedBackManageSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FeedBackManageSystem.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        public FeedbackController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
              _context= context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var feebacklists = _context.tblFeedback.ToList();
            return View(feebacklists); 
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(FeedbackViewModel feedback)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string? filePath = null;
                    if (feedback.ImageFile != null && feedback.ImageFile.Length > 0)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                        var extensions = Path.GetExtension(feedback.ImageFile.FileName).ToLower();
                        if (!allowedExtensions.Contains(extensions))
                        {
                            ModelState.AddModelError("Imagefile","Only jpg and png files are allowed");
                            return View(feedback);
                        }
                        if(feedback.ImageFile.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("Imagefile", "Image size must be less than 2MB.");
                            return View(feedback);
                        }
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        Directory.CreateDirectory(uploads);

                        var filename = Guid.NewGuid().ToString() + extensions;
                        var fullPath = Path.Combine(uploads, filename);

                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            feedback.ImageFile.CopyTo(fileStream);
                        };
                        feedback.ImagePath = "/images/" + filename;
                    }

                    _context.tblFeedback.Add(feedback);
                    _context.SaveChanges();
                    return RedirectToAction("ThankYou");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            return View(feedback);
        }
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
