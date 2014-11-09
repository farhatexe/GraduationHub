using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Models.Students;

namespace GraduationHub.Web.Controllers
{
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
    }
}