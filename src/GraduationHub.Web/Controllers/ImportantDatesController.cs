using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models.ImportantDates;

namespace GraduationHub.Web.Controllers
{
    [GraduationHubAuthorize(Roles = "Teachers, Admin")]
    public class ImportantDatesController : AppBaseController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ImportantDatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImportantDates
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> IndexTable(
            [ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            // Query
            IQueryable<ImportantDateIndexModel> query = _context.ImportantDates
                .Project().To<ImportantDateIndexModel>()
                .OrderBy(requestModel.Sort());

            if (requestModel.HasSearchValues())
            {
                query = query.Where(requestModel.SearchValues(), requestModel.Search.Value);
            }

            // Data
            List<ImportantDateIndexModel> data = await query.ToListAsync();

            int totalRecords = data.Count();

            IEnumerable<ImportantDateIndexModel> paged = data.Skip(requestModel.Start).Take(requestModel.Length);

            var response = new DataTablesResponse(requestModel.Draw, paged, totalRecords, totalRecords);

            return JsonSuccess(response, JsonRequestBehavior.AllowGet);
        }

        // GET: ImportantDates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImportantDates/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImportantDateCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var importantDate = new ImportantDate
            {
                DueDate = model.DueDate,
                Comments = model.Comments
            };

            _context.ImportantDates.Add(importantDate);

            await _context.SaveChangesAsync();

            return RedirectToAction<ImportantDatesController>(c => c.Index())
                .WithSuccess("Important Date Created.");
        }

        // GET: ImportantDates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ImportantDateEditModel model = await _context.ImportantDates.Project().To<ImportantDateEditModel>()
                .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: ImportantDates/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ImportantDateEditModel model)
        {
            if (!ModelState.IsValid) return View(model);

            ImportantDate importantDate = await _context.ImportantDates.SingleOrDefaultAsync(i => i.Id == model.Id);

            if (importantDate == null)
            {
                return RedirectToAction<ImportantDatesController>(c => c.Index())
                    .WithError("Could not load Important Date");
            }

            importantDate.DueDate = model.DueDate;
            importantDate.Comments = model.Comments;

            await _context.SaveChangesAsync();

            return RedirectToAction<ImportantDatesController>(c => c.Index())
                .WithSuccess("Important Date Updated.");
        }

        // GET: ImportantDates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await _context.ImportantDates.Project().To<ImportantDateDeleteModel>()
                .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: ImportantDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ImportantDate importantDate = await _context.ImportantDates.FindAsync(id);
            
            _context.ImportantDates.Remove(importantDate);
            
            await _context.SaveChangesAsync();

            return RedirectToAction<ImportantDatesController>(c => c.Index())
                .WithSuccess("Important Date Deleted.");
        }
    }
}