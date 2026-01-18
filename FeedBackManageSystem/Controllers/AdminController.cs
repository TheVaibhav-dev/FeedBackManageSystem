using FeedBackManageSystem.Enum;
using FeedBackManageSystem.HelperClasses.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FeedBackManageSystem.Controllers
{
    [RoleAuthorize(UserType.Admin)]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
