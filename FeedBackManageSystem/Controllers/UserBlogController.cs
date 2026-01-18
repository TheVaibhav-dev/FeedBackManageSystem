using FeedBackManageSystem.Enum;
using FeedBackManageSystem.HelperClasses;
using FeedBackManageSystem.HelperClasses.Filters;
using FeedBackManageSystem.Models;
using FeedBackManageSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FeedBackManageSystem.Controllers
{
    [RoleAuthorize(UserType.Admin,UserType.Author)]
    public class UserBlogController : Controller
    {
        private IUserBlogService _userBlogService;
        private readonly IUserService _userService;
        public UserBlogController(IUserBlogService userBlogService, IUserService userService)
        {
            _userBlogService = userBlogService;
            _userService = userService;
        }
        public IActionResult Dashboard()
        {
            var idUser = ProjectSession.UserId;
            var model = _userService.GetUser(idUser);
            var blogsData = _userBlogService.GetById(idUser);
            var monthlyData = _userBlogService.GetMonthlyPairs((long)idUser);

            int[] monthlyBlogs = new int[12];
            for(int i=1; i<=12;i++)
            {
                monthlyBlogs[i - 1] = monthlyData.ContainsKey(i)
                    ? monthlyData[i] : 0;
            }
            var blogs = new UserDashboardViewModel()
            {
                UserName =  ProjectSession.UserType == UserType.Admin ? "Admin" : model.FullName,
                TotalBlogs = blogsData.Count,
                TotalRatings = blogsData.Sum(x => x.StarRating),
                Blogs = blogsData,
                MonthlyBlogs = monthlyBlogs
            };

            return View(blogs);
        }
        [HttpGet]
        public IActionResult AddBlogs()
        {
            var model= _userBlogService.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddBlogs(UserBlogViewModel model)
        {
            model.IdUser = (long)ProjectSession.UserId;
            var blogs = _userBlogService.Create(model);
            return View(blogs);
        }
    }
}
