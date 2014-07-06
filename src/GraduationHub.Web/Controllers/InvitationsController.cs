using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Models.Invitations;

namespace GraduationHub.Web.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvitationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Invitations
        public async Task<ActionResult> Index()
        {
            return View(await _context.Invitations.Project().To<IndexViewModel>().ToListAsync());
        }

        // GET: Invitations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // GET: Invitations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,StudentName,GraduationYear,Email,InviteCode,HasBeenRedeemed,HasBeenSent")] Invitation
                invitation)
        {
            if (ModelState.IsValid)
            {
                _context.Invitations.Add(invitation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(invitation);
        }

        // GET: Invitations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,StudentName,GraduationYear,Email,InviteCode,HasBeenRedeemed,HasBeenSent")] Invitation
                invitation)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(invitation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Invitation invitation = await _context.Invitations.FindAsync(id);
            _context.Invitations.Remove(invitation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}