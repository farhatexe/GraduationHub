using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Models.GraduateInformation;

namespace GraduationHub.Web.Controllers
{
    [GraduationHubAuthorize(Roles = "Teacher, Admin")]
    public class GraduateInformationController : AppBaseController
    {
        private readonly ApplicationDbContext _dbContext;

        public GraduateInformationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexTable([ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<GraduateIndexModel> data =
                _dbContext.GraduateInformation.Include(i => i.Student)
                    .Project()
                    .To<GraduateIndexModel>().OrderBy(requestModel.Sort());

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