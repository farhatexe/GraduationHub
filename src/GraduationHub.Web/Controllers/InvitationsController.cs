using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models.Invitations;
using Postal;

namespace GraduationHub.Web.Controllers
{
   [GraduationHubAuthorize(Roles = "Teachers, Admin")]
    public class InvitationsController : AppBaseController
    {
        private readonly ApplicationDbContext _context;

        public InvitationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Invitations
        public ActionResult Index()
        {
            //return View(await _context.Invitations.Project().To<InvitationIndexViewModel>().ToListAsync());
            return View();
        }

        public async Task<ActionResult> IndexTable([ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            // Query
            IQueryable<InvitationIndexViewModel> query = _context.Invitations
                .Project().To<InvitationIndexViewModel>()
                .OrderBy(requestModel.Sort());

            if (requestModel.HasSearchValues())
            {
                query = query.Where(requestModel.SearchValues(), requestModel.Search.Value);
            }

            // Data
            List<InvitationIndexViewModel> data = await query.ToListAsync();

            int totalRecords = data.Count();

            IEnumerable<InvitationIndexViewModel> paged = data.Skip(requestModel.Start).Take(requestModel.Length);

            var response = new DataTablesResponse(requestModel.Draw, paged, totalRecords, totalRecords);

            return JsonSuccess(response, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> Create(InvitationCreateFormModel formModel)
        {
            if (!ModelState.IsValid) return View(formModel);

            // If the email is already being used to authenticate into the system, it cannot be used
            bool userExists = await _context.Users
                .AnyAsync(u => u.Email.Equals(formModel.Email, StringComparison.OrdinalIgnoreCase));


            if (userExists)
            {
                return RedirectToAction<InvitationsController>(c => c.Index())
                    .WithError("It appears there is an existing User with this email address.");
            }
            // if there is an invitation for the same year, same email, this is invalid.
            bool invitationExists = await _context.Invitations.AnyAsync(i =>
                i.Email.Equals(formModel.Email, StringComparison.OrdinalIgnoreCase));

            if (invitationExists)
            {
                return RedirectToAction<InvitationsController>(c => c.Index())
                    .WithError("It appears that this email has an invitation already.");
            }


            var invitation = new Invitation
            {
                InviteeName = formModel.InviteeName,
                Email = formModel.Email,
                IsTeacher = formModel.IsTeacher,
                InviteCode = Guid.NewGuid()
                
            };

            _context.Invitations.Add(invitation);

            await _context.SaveChangesAsync();

            return RedirectToAction<InvitationsController>(c => c.Index()).WithSuccess("Invitation Created");
        }

        // GET: Invitations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvitationEditFormModel invitation =
                await _context.Invitations.Project().To<InvitationEditFormModel>()
                    .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (invitation == null)
            {
                return RedirectToAction<InvitationsController>(c => c.Index())
                    .WithError("Could not load the Invitation.");
            }
            return View(invitation);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InvitationEditFormModel editModel)
        {
            if (!ModelState.IsValid) return View(editModel);

            // If the email is already being used to authenticate into the system, it cannot be used
            bool userExists = await _context.Users
                .AnyAsync(u => u.Email.Equals(editModel.Email, StringComparison.OrdinalIgnoreCase));


            if (userExists)
            {
                return RedirectToAction<InvitationsController>(c => c.Index())
                    .WithError("It appears there is an existing User with this email address.");
            }

            // if there is an invitation for the same year, same email, this is invalid.
            bool invitationExists = await _context.Invitations.AnyAsync(i =>
                i.Email.Equals(editModel.Email, StringComparison.OrdinalIgnoreCase)
                && i.Id != editModel.Id);

            if (invitationExists)
            {
                return RedirectToAction<InvitationsController>(c => c.Index())
                    .WithError("It appears that this email has an invitation already.");
            }
            Invitation invitation = await _context.Invitations.SingleOrDefaultAsync(i => i.Id == editModel.Id);

            if (invitation == null)
            {
                return RedirectToAction<InvitationsController>(c => c.Index())
                    .WithError("Could not load the Invitation.");
            }
            invitation.InviteeName = editModel.InviteeName;
            invitation.Email = editModel.Email;
            invitation.IsTeacher = editModel.IsTeacher;
            invitation.HasBeenRedeemed = editModel.HasBeenRedeemed;

            _context.Entry(invitation).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return RedirectToAction<InvitationsController>(c => c.Index()).WithSuccess("Invitation updated.");
        }

        // GET: Invitations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = await _context.Invitations
                .Project().To<InvitationDeleteFormModel>()
                .SingleAsync(i => i.Id == id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
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

       public async Task<ActionResult> Send(int id)
       {
           // get the invite:
           var invitation = await _context.Invitations.SingleAsync(i => i.Id == id);

           dynamic email = invitation.IsTeacher ? new Email("TeacherInvitation"): new Email("StudentInvitation");

           email.To = invitation.Email;
           email.InviteeName = invitation.InviteeName;
           email.InviteCode = invitation.InviteCode;
           email.Send();

           invitation.HasBeenSent = true;
           _context.Entry(invitation).State = EntityState.Modified;
           await _context.SaveChangesAsync();

           return RedirectToAction("Index").WithSuccess("Invitation has been sent.");
       }
    }
}