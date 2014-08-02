using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models.FrequentlyAskedQuestions;
using GraduationHub.Web.Models.StudentExpressions;

namespace GraduationHub.Web.Controllers
{
    public class StudentExpressionsController : AppBaseController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly ICurrentUser _currentUser;

        public StudentExpressionsController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: StudentExpressions
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> IndexTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            
                var currentUser = _currentUser.User;

                // Query
                var query = _context.StudentExpressions
                    .Where(i => i.StudentId == currentUser.Id)
                    .Project().To<StudentExpressionIndexModel>()
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


        // GET: StudentExpressions/Create
        public ActionResult Create()
        {


            return View(new StudentExpressionCreateModel());
        }

        // POST: StudentExpressions/Create
        [HttpPost, ValidateAntiForgeryToken, Log("Create Student Expression") ]
        public async Task<ActionResult> Create(StudentExpressionCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var studentExpression = new StudentExpression
            {
                Student = _currentUser.User,
                Text = model.Text,
                DateSubmitted = DateTime.Now,
                Type = model.Type
            };

            _context.StudentExpressions.Add(studentExpression);

            await _context.SaveChangesAsync();


            return RedirectToAction<StudentExpressionsController>(c => c.Index())
                .WithSuccess("Student Expression Created.");
        }

        // GET: StudentExpressions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await _context.StudentExpressions
                .Project().To<StudentExpressionEditModel>()
                .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: StudentExpressions/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentExpressionEditModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var studentExpression = await _context.StudentExpressions.SingleOrDefaultAsync(i => i.Id == model.Id);

            if (studentExpression == null)
            {
                return RedirectToAction<StudentExpressionsController>(c => c.Index())
                    .WithError("Could not load the Student Expression");
            }

            studentExpression.Text = model.Text;

            _context.Entry(studentExpression).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();

            return RedirectToAction<StudentExpressionsController>(c => c.Index())
                .WithSuccess("Student Expression Edited.");
        }

        // GET: StudentExpressions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await _context.StudentExpressions.Project()
                .To<StudentExpressionDeleteModel>()
                .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: StudentExpressions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StudentExpression studentExpression = await _context.StudentExpressions.FindAsync(id);
            
            _context.StudentExpressions.Remove(studentExpression);
            
            await _context.SaveChangesAsync();

            return RedirectToAction<StudentExpressionsController>(c => c.Index())
                .WithSuccess("Student Expression Deleted.");
        }

    }
}
