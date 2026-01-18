using FeedBackManageSystem.Enum;
using FeedBackManageSystem.HelperClasses;
using FeedBackManageSystem.Models;
using FeedBackManageSystem.Repositories.Interface;
using FeedBackManageSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FeedBackManageSystem.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var isCreated = _userService.Create(model);
                if (!isCreated)
                {
                    TempData["Error"] = "An error occurred while adding a New User. Please try again.";
                    return RedirectToAction("Login");
                }
                TempData["Success"] = "New User added successfully.";
                return RedirectToAction("ThankYou","Feedback");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Email and password required";
                return View();
            }
            var User = _userService.Authenticate(email, password);

            if (User == null)
            {
                TempData["Error"]= "Invalid email or password.";
                return View();
            }

            ProjectSession.UserId = User.Id;
            ProjectSession.UserType = User.UserType;

            return User.UserType switch
            {
                UserType.Admin => RedirectToAction("Dashboard", "Admin"),
                UserType.Author => RedirectToAction("Dashboard", "UserBlog"),
                _ => RedirectToAction("Landing", "Home")
            };
        }
        public IActionResult Logout() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("MyVisitor", "Visitor");
        }
    }
}
