using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Models.CheckList;
using GraduationHub.Web.Models.StudentExpressions;

namespace GraduationHub.Web.Controllers
{
    public class CheckListController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CheckListController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Biography()
        {
            return View();
        }

        public ActionResult ExpressionOfThanks()
        {
            return View(new StudentExpressionCreateModel{TextMaxLength = 100 });
        }

        public ActionResult SlideShowCaption()
        {
            return View();
        }

        public ActionResult SeniorPortrait()
        {
            return View();
        }

        public ActionResult BabyPicture()
        {
            return View();
        }

        public ActionResult FirstYouthfulPicture()
        {
            return View();
        }

        public ActionResult SecondYouthfulPicture()
        {
            return View();
        }
        
        public ActionResult ThirdYouthfulPicture()
        {
            return View();
        }

        public ActionResult FourthYouthfulPicture()
        {
            return View();
        }

        public ActionResult ImportantDates()
        {
            // TODO: Add Student's Graduating Class in case we need to split large groups
            var dueDates = _dbContext.ImportantDates.Project().To<CheckListImportantDate>().OrderBy(x=>x.DueDate).ToList();

            return View(dueDates);
        }

        public ActionResult Faqs()
        {
            var faqs = _dbContext.FrequentlyAskedQuestions.OrderBy(x => x.Order).Project().To<Faq>().ToList();

            return View(faqs);
        }
    }
}