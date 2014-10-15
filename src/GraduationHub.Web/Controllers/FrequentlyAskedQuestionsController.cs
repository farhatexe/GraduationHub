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
using GraduationHub.Web.Models.FrequentlyAskedQuestions;

namespace GraduationHub.Web.Controllers
{
    [GraduationHubAuthorize(Roles = "Teacher, Admin")]
    public class FrequentlyAskedQuestionsController : AppBaseController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public FrequentlyAskedQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FrequentlyAskedQuestions
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> IndexTable(
            [ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            // Query
            IQueryable<FrequentlyAskedQuestionIndexModel> query = _context.FrequentlyAskedQuestions
                .Project().To<FrequentlyAskedQuestionIndexModel>()
                .OrderBy(requestModel.Sort());

            if (requestModel.HasSearchValues())
            {
                query = query.Where(requestModel.SearchValues(), requestModel.Search.Value);
            }

            // Data
            List<FrequentlyAskedQuestionIndexModel> data = await query.ToListAsync();

            int totalRecords = data.Count();

            IEnumerable<FrequentlyAskedQuestionIndexModel> paged =
                data.Skip(requestModel.Start).Take(requestModel.Length);

            var response = new DataTablesResponse(requestModel.Draw, paged, totalRecords, totalRecords);

            return JsonSuccess(response, JsonRequestBehavior.AllowGet);
        }

        // GET: FrequentlyAskedQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrequentlyAskedQuestions/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FrequentlyAskedQuestionCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var frequentlyAskedQuestion = new FrequentlyAskedQuestion { Order = model.Order, Question = model.Question, Answer = model.Answer};

            _context.FrequentlyAskedQuestions.Add(frequentlyAskedQuestion);

            await _context.SaveChangesAsync();

            return RedirectToAction<FrequentlyAskedQuestionsController>(c => c.Index())
                .WithSuccess("Frequently Asked Question Created.");
        }

        // GET: FrequentlyAskedQuestions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FrequentlyAskedQuestionEditModel model = await _context.FrequentlyAskedQuestions.Project()
                .To<FrequentlyAskedQuestionEditModel>()
                .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FrequentlyAskedQuestionEditModel model)
        {
            if (!ModelState.IsValid) return View(model);

            FrequentlyAskedQuestion faq =
                await _context.FrequentlyAskedQuestions.SingleOrDefaultAsync(i => i.Id == model.Id);

            if (faq == null)
            {
                return RedirectToAction<FrequentlyAskedQuestionsController>(c => c.Index())
                    .WithError("Could not load the Frequently Asked Question");
            }

            faq.Question = model.Question;
            faq.Answer = model.Answer;
            faq.Order = model.Order;

            await _context.SaveChangesAsync();

            return RedirectToAction<FrequentlyAskedQuestionsController>(c => c.Index())
                .WithSuccess("Frequently Asked Question Edited.");
        }

        // GET: FrequentlyAskedQuestions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FrequentlyAskedQuestionDeleteModel model = await _context.FrequentlyAskedQuestions.Project()
                .To<FrequentlyAskedQuestionDeleteModel>()
                .SingleOrDefaultAsync(i => i.Id == id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: FrequentlyAskedQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FrequentlyAskedQuestion frequentlyAskedQuestion = await _context.FrequentlyAskedQuestions.FindAsync(id);

            _context.FrequentlyAskedQuestions.Remove(frequentlyAskedQuestion);

            await _context.SaveChangesAsync();

            return RedirectToAction<FrequentlyAskedQuestionsController>(c => c.Index())
                .WithSuccess("Frequently Asked Question Deleted.");
        }
    }
}