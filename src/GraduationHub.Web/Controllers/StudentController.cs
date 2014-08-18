using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Models.Students;

namespace GraduationHub.Web.Controllers
{
    //TODO: Add the Authorize Student Attribute...they must be in the student role.
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private List<StudentExpression> _studentExpressions;


        public StudentController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: Student
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult MyBiography()
        {
            ApplicationUser currentUser = _currentUser.User;

            StudentExpressionModel studentBiography = _context.StudentExpressions
                .Where(i => i.Type == StudentExpressionType.Biography && i.StudentId == currentUser.Id)
                .Project().To<StudentExpressionModel>().SingleOrDefault() ?? new StudentExpressionModel();

            return View(studentBiography);
        }

        [ChildActionOnly]
        public ActionResult MySeniorPicture()
        {
            return View();
        }

        public ActionResult GetSeniorPicture()
        {
            ApplicationUser currentUser = _currentUser.User;

            var webImage = new WebImage("~/Content/Images/male_silhouette.png");
            webImage.Resize(300, 300, false, true);
            
            var model = _context.StudentPictures
                .Where(i => i.ImageType == StudentPictureType.SeniorPortrait && i.StudentId == currentUser.Id)
                .Project().To<StudentPictureModel>().SingleOrDefault() ?? new StudentPictureModel();

            if (model.ImageData == null)
            {
                model.ImageName = "male_silhouette.png";
                model.Description = "Senior Portrait";
                model.ImageData = webImage.GetBytes();
            }

            return new FileStreamResult(new MemoryStream(model.ImageData), model.ContentType);
        }

    }
}