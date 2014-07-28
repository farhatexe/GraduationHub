using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;

namespace GraduationHub.Web.Controllers
{
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
            IQueryable<ImportantDate> importantDates = _context.ImportantDates.Include(i => i.GraduatingClass);
            return View(await importantDates.ToListAsync());
        }

        // GET: ImportantDates/Create
        public ActionResult Create()
        {
            ViewBag.GraduatingClassId = new SelectList(_context.GraduatingClasses, "Id", "Description");
            return View();
        }

        // POST: ImportantDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Create Important Date")]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,DueDate,Description,GraduatingClassId")] ImportantDate importantDate)
        {
            if (!ModelState.IsValid) return View(importantDate);

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
            ImportantDate importantDate = await _context.ImportantDates.FindAsync(id);
            if (importantDate == null)
            {
                return HttpNotFound();
            }
            ViewBag.GraduatingClassId = new SelectList(_context.GraduatingClasses, "Id", "Description",
                importantDate.GraduatingClassId);
            return View(importantDate);
        }

        // POST: ImportantDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Edit Important Date")]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,DueDate,Description,GraduatingClassId")] ImportantDate importantDate)
        {
            if (!ModelState.IsValid) return View(importantDate);

            _context.Entry(importantDate).State = EntityState.Modified;

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
            ImportantDate importantDate = await _context.ImportantDates.FindAsync(id);
            if (importantDate == null)
            {
                return HttpNotFound();
            }
            return View(importantDate);
        }

        // POST: ImportantDates/Delete/5
        [HttpPost, ActionName("Delete"), Log("Deleted Important Date")]
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