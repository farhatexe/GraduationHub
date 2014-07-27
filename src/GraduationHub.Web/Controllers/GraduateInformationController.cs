using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;

namespace GraduationHub.Web.Controllers
{
    public class GraduateInformationController : AppBaseController
    {
        private readonly ApplicationDbContext _context;

        public GraduateInformationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GraduateInformation
        public async Task<ActionResult> Index()
        {
            return View(await _context.GraduateInformation.ToListAsync());
        }

        // GET: GraduateInformation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GraduateInformation graduateInformation = await _context.GraduateInformation.FindAsync(id);
            if (graduateInformation == null)
            {
                return HttpNotFound();
            }
            return View(graduateInformation);
        }

        // GET: GraduateInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GraduateInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Address,StudentEmail,ParentEmail,EnrolledFineArts,EnrolledAcademicClasses,WillParticiateInGraduation,TakenKeysWorldView,TakenApprovedWorldView,WillSecureAnnouncements,NeedCapAndGown,Height")] GraduateInformation graduateInformation)
        {
            if (ModelState.IsValid)
            {
                _context.GraduateInformation.Add(graduateInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(graduateInformation);
        }

        // GET: GraduateInformation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GraduateInformation graduateInformation = await _context.GraduateInformation.FindAsync(id);
            if (graduateInformation == null)
            {
                return HttpNotFound();
            }
            return View(graduateInformation);
        }

        // POST: GraduateInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,StudentEmail,ParentEmail,EnrolledFineArts,EnrolledAcademicClasses,WillParticiateInGraduation,TakenKeysWorldView,TakenApprovedWorldView,WillSecureAnnouncements,NeedCapAndGown,Height")] GraduateInformation graduateInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(graduateInformation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(graduateInformation);
        }

        // GET: GraduateInformation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GraduateInformation graduateInformation = await _context.GraduateInformation.FindAsync(id);
            if (graduateInformation == null)
            {
                return HttpNotFound();
            }
            return View(graduateInformation);
        }

        // POST: GraduateInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GraduateInformation graduateInformation = await _context.GraduateInformation.FindAsync(id);
            _context.GraduateInformation.Remove(graduateInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
