using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Models.GraduateInformation;
using System.Data.Entity;
using System.Linq.Dynamic;

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
            try
            {
                IQueryable<GraduateIndexModel> data =
                    _dbContext.GraduateInformation.Include(i => i.Student)
                        .Project()
                        .To<GraduateIndexModel>().OrderBy(requestModel.Sort());

                int totalRecords = data.Count();

                IQueryable<GraduateIndexModel> paged =
                    data.Skip(requestModel.Start).Take(requestModel.Length);

                var response = new DataTablesResponse(requestModel.Draw, paged, totalRecords, totalRecords);

                return JsonSuccess(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}