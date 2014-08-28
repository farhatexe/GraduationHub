using System.Web.Mvc;
using GraduationHub.Web.Models.StudentExpressions;

namespace GraduationHub.Web.Controllers
{
    public class CheckListController : Controller
    {
        

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
            return View();
        }

        public ActionResult Faqs()
        {
            return View();
        }
    }
}