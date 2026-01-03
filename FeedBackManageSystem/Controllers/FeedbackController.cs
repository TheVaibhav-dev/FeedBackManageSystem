using FeedBackManageSystem.Data;
using FeedBackManageSystem.HelperClasses;
using FeedBackManageSystem.Models;
using FeedBackManageSystem.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FeedBackManageSystem.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _service;
        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }
        /// <summary>
        /// Created By : Vaibhav Srivastava
        /// Created Date : 23-12-2025
        /// Description : To fetch all feedback entries from the database and display them in the view.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            var model = _service.GetAll();
            return View(model); 
        }
        //[HttpPost]
        //public JsonResult SearchFeedbackData(DataTableAjaxPostModel model, DateTime? startDate, DateTime? enddate)
        //{
        //    var resultSet = _service.SearchFeedbackdata(model, startDate, enddate);
        //    var output = new DataTableAjaxPostModel()
        //    {
        //        draw = model.draw,
        //        recordsTotal = resultSet.RecordsTotal,
        //        recordsFiltered = resultSet.RecordsFiltered,
        //        data = resultSet.Data
        //    };
        //    return Json(output);
        //}
        /// <summary>
        /// Created By : Vaibhav Srivastava
        /// Created Date :24-12-2025
        /// Description : To display the feedback submission form.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(FeedbackViewModel feedback)
        {
           
                if (!ModelState.IsValid)
                    return View(feedback);
            try
            {
                var isUpdate = _service.Create(feedback);
                if (!isUpdate)
                {
                    TempData["UpdateError"] = "An error occurred while creating feedback. Please try again.";
                    return RedirectToAction("Update", new { id = Encryption.EncyptNumber(feedback.Id) });
                }
                TempData["SuccessMessage"] = "Feedback uploaded Successfully";
                return RedirectToAction("Thankyou");
            }
            catch (Exception ex)
            {
                TempData["UpdateError"] = ex.Message;
                return RedirectToAction("Update", new { id = Encryption.EncyptNumber(feedback.Id) });
            }
        }
        public IActionResult ThankYou()
        {
            return View();
        }
        /// <summary>
        /// Created By : Vaibhav Srivastava
        /// Created Date : 26-12-2025
        /// Description : To edit an existing feedback entry.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(string id)
        {
            var decNum= Encryption.DecryptNumber(id);
            var feedback = _service.GetById((int)decNum);
            if (feedback == null)
                return NotFound();

            return View(feedback);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(string id,FeedbackViewModel model)
        {
            model.Id = (int)Encryption.DecryptNumber(id);

            ModelState.Remove(nameof(model.Id));
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var isUpdate = _service.Update(model);
                if (!isUpdate)
                {
                    TempData["UpdateError"] = "An error occurred while updating feedback. Please try again.";
                    return RedirectToAction("Update", new { id =Encryption.EncyptNumber(model.Id)});
                }
                TempData["SuccessMessage"] = "Feedback updated Successfully";
                return RedirectToAction("Thankyou");
            }
            catch (Exception ex)
            {
                TempData["UpdateError"] = ex.Message;
                return RedirectToAction("Update", new { id = Encryption.EncyptNumber(model.Id) });
            }
        }
        /// <summary>
        /// Created By : Vaibhav Srivastava
        /// Created Date : 26-12-2025
        /// Description : To Delete an existing feedback entry.
        /// </summary>
        public IActionResult Delete(string id)
        {
            _service.Delete((int)Encryption.DecryptNumber(id));
            return RedirectToAction("Index");
        }
    }
}
