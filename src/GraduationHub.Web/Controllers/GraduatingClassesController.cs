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
using GraduationHub.Web.Models.GraduatingClass;

namespace GraduationHub.Web.Controllers
{
    public class GraduatingClassesController : AppBaseController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public GraduatingClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GraduatingClasses
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> IndexTable(
            [ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            // Query
            var query = _context.GraduatingClasses
                .Project().To<GraduatingClassIndexModel>()
                .OrderBy(requestModel.Sort());

            if (requestModel.HasSearchValues())
            {
                query = query.Where(requestModel.SearchValues(), requestModel.Search.Value);
            }

            // Data
            var data = await query.ToListAsync();

            int totalRecords = data.Count();

            var paged = data.Skip(requestModel.Start).Take(requestModel.Length);

            var response = new DataTablesResponse(requestModel.Draw, paged, totalRecords, totalRecords);

            return JsonSuccess(response, JsonRequestBehavior.AllowGet);
        }

        // GET: GraduatingClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GraduatingClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Created Invitation")]
        public async Task<ActionResult> Create(GraduatingClassCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var graduatingClass = new GraduatingClass {Description = model.Description};

            _context.GraduatingClasses.Add(graduatingClass);

            await _context.SaveChangesAsync();

            return RedirectToAction<GraduatingClassesController>(c => c.Index())
                .WithSuccess("Graduating Class Created.");
        }

        // GET: GraduatingClasses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invitation =
                await _context.GraduatingClasses.Project().To<GraduatingClassEditModel>()
                    .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: GraduatingClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Edited Graduating Class")]
        public async Task<ActionResult> Edit(GraduatingClassEditModel model)
        {
            if (!ModelState.IsValid) return View(model);

            GraduatingClass graduatingClass =
                await _context.GraduatingClasses.SingleOrDefaultAsync(i => i.Id == model.Id);

            if (graduatingClass == null)
            {
                return RedirectToAction<GraduatingClassesController>(c => c.Index())
                    .WithError("Could not load the Graduating Class.");
            }

            graduatingClass.Description = model.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction<GraduatingClassesController>(c => c.Index())
                .WithSuccess("Graduating Class Updated.");
        }

        // GET: GraduatingClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GraduatingClassDeleteModel graduatingClass = await _context.GraduatingClasses
                .Project().To<GraduatingClassDeleteModel>()
                .SingleAsync(i => i.Id == id);

            if (graduatingClass == null)
            {
                return HttpNotFound();
            }
            return View(graduatingClass);
        }

        // POST: GraduatingClasses/Delete/5
        [HttpPost, ActionName("Delete"), Log("Deleted Graduating Class")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GraduatingClass graduatingClass = await _context.GraduatingClasses.FindAsync(id);
            _context.GraduatingClasses.Remove(graduatingClass);
            await _context.SaveChangesAsync();

            return RedirectToAction<GraduatingClassesController>(c => c.Index())
                .WithSuccess("Graduating Class Deleted.");
        }
    }
}