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

namespace GraduationHub.Web.Controllers
{
    public class FrequentlyAskedQuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FrequentlyAskedQuestions
        public async Task<ActionResult> Index()
        {
            return View(await db.FrequentlyAskedQuestions.ToListAsync());
        }

        // GET: FrequentlyAskedQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrequentlyAskedQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Question,Answer")] FrequentlyAskedQuestion frequentlyAskedQuestion)
        {
            if (ModelState.IsValid)
            {
                db.FrequentlyAskedQuestions.Add(frequentlyAskedQuestion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(frequentlyAskedQuestion);
        }

        // GET: FrequentlyAskedQuestions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrequentlyAskedQuestion frequentlyAskedQuestion = await db.FrequentlyAskedQuestions.FindAsync(id);
            if (frequentlyAskedQuestion == null)
            {
                return HttpNotFound();
            }
            return View(frequentlyAskedQuestion);
        }

        // POST: FrequentlyAskedQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Question,Answer")] FrequentlyAskedQuestion frequentlyAskedQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frequentlyAskedQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(frequentlyAskedQuestion);
        }

        // GET: FrequentlyAskedQuestions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrequentlyAskedQuestion frequentlyAskedQuestion = await db.FrequentlyAskedQuestions.FindAsync(id);
            if (frequentlyAskedQuestion == null)
            {
                return HttpNotFound();
            }
            return View(frequentlyAskedQuestion);
        }

        // POST: FrequentlyAskedQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FrequentlyAskedQuestion frequentlyAskedQuestion = await db.FrequentlyAskedQuestions.FindAsync(id);
            db.FrequentlyAskedQuestions.Remove(frequentlyAskedQuestion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
