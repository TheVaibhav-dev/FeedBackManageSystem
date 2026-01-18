using FeedBackManageSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FeedBackManageSystem.Controllers
{
    public class VisitorController : Controller
    {
        private readonly IUserBlogService _blogService;
        public VisitorController(IUserBlogService blogService)
        {
            _blogService = blogService;
        }
        public IActionResult MyVisitor()
        {
            var model= _blogService.GetAll();
            return View(model);
        }
        public IActionResult BlogById(long id)
        {
            return View(_blogService.GetById((int)id));
        }
    }
}
