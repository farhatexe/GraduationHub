using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models.Students;

namespace GraduationHub.Web.Controllers
{
    [GraduationHubAuthorize(Roles = "Teacher, Admin")]
    public class StudentsController : AppBaseController
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexTable([ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<StudentIndexModel> data =
                _dbContext.Users
                    .Include(i => i.StudentExpressions)
                    .Include(i => i.StudentPictures)
                    .Where(x => x.IsStudent)
                    .Project()
                    .To<StudentIndexModel>().OrderBy(requestModel.Sort());

            if (requestModel.HasSearchValues())
            {
                data = data.Where(requestModel.SearchValues(), requestModel.Search.Value);
            }

            int totalRecords = data.Count();

            var response = new DataTablesResponse(requestModel.Draw, data, totalRecords, totalRecords);

            return JsonSuccess(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentInformation(string id)
        {
            var studentInfo = _dbContext.GraduateInformation.Project().To<StudentInformationModel>().Single(x => x.StudentId.Equals(id));

            return View(studentInfo).WithInfo("This is to demonstrate what the final app will look like.");
        }


        public ActionResult Biography(string id)
        {
            return View();
        }

        public ActionResult ExpressionOfThanks(string id)
        {
            return View();
        }

        public ActionResult SlideShowCaption(string id)
        {
            return View();
        }

        public ActionResult SeniorPortrait(string id)
        {
            return View();
        }

        public ActionResult BabyPicture(string id)
        {
            return View();
        }

        public ActionResult ToddlerPicture(string id)
        {
            return View();
        }

        public ActionResult ElementaryPicture(string id)
        {
            return View();
        }

        public ActionResult MiddleSchoolPicture(string id)
        {
            return View();
        }

        public ActionResult HighSchoolPicture(string id)
        {
            return View();
        }
    }
}