using FeedBackManageSystem.HelperClasses;
using FeedBackManageSystem.Models;
using FeedBackManageSystem.Repositories.Interface;
using FeedBackManageSystem.Services.Interface;
using Microsoft.AspNetCore.Hosting;

namespace FeedBackManageSystem.Services.Service
{


    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FeedbackService(IFeedbackRepository repo,IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IList<FeedbackViewModel> GetAll()
            => _repo.GetAll();

        public FeedbackViewModel GetById(int id)
            => _repo.GetById(id);

        public bool Create(FeedbackViewModel feedback)
        {
            try
            {
                if (feedback.ImageFile != null && feedback.ImageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var ext = Path.GetExtension(feedback.ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(ext))
                        throw new Exception("Invalid file type. Only JPG, JPEG, and PNG are allowed.");

                    if (feedback.ImageFile.Length > 2 * 1024 * 1024)
                        throw new Exception("File size exceeds 2MB limit");

                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    Directory.CreateDirectory(uploads);

                    var fileName = $"{Guid.NewGuid()}{ext}";
                    var fullPath = Path.Combine(uploads, fileName);

                    using var fs = new FileStream(fullPath, FileMode.Create);
                    feedback.ImageFile.CopyTo(fs);

                    feedback.ImagePath = "/images/" + fileName;
                }
                feedback.IsActive = true;
                feedback.DateCreated= DateTime.Now;
                _repo.Add(feedback);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(FeedbackViewModel feedback)
        {
            try
            {
                var existing = _repo.GetById(feedback.Id);

                if (existing == null)
                    throw new Exception("Record not found");

                if (feedback.ImageFile != null && feedback.ImageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var ext = Path.GetExtension(feedback.ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(ext))
                        throw new Exception("Invalid file type. Only JPG, JPEG, and PNG are allowed.");

                    if (feedback.ImageFile.Length > 2 * 1024 * 1024)
                        throw new Exception("File size exceeds 2MB limit");

                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    Directory.CreateDirectory(uploads);

                    var newFileName = $"{Guid.NewGuid()}{ext}";
                    var fullPath = Path.Combine(uploads, newFileName);

                    using var fs = new FileStream(fullPath, FileMode.Create);
                    feedback.ImageFile.CopyTo(fs);

                    if (!string.IsNullOrEmpty(existing.ImagePath))
                    {
                        var oldPath = Path.Combine(
                            _webHostEnvironment.WebRootPath,
                            existing.ImagePath.TrimStart('/')
                        );

                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    feedback.ImagePath = "/images/" + newFileName;
                }
                else
                {
                    feedback.ImagePath = existing.ImagePath;
                }
                feedback.IsActive = true;
                feedback.DateCreated = DateTime.Now;
                _repo.Update(feedback);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public void Delete(int id)
            => _repo.Delete(id);

        //public DataTableResult<FeedbackViewModel> SearchFeedbackdata(DataTableAjaxPostModel model, DateTime? startDate, DateTime? endDate)
        //{
        //    var data = _repo.GetAll();
        //    return DataTableHelper.ApplyDataTable<FeedbackViewModel>(data, model, x => x.DateCreated!, startDate, endDate);
        //}

    }

}
