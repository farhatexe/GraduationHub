using System.Web.Mvc;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;

namespace GraduationHub.Web.Controllers
{
    public class HomeController : Controller
    {
        [GraduationHubAuthorize(Roles = SecurityConstants.Roles.Student)]
        public ActionResult Index()
        {
            return View();
        }

        [GraduationHubAuthorize(Roles = SecurityConstants.Roles.Teacher)]
        public ActionResult Teacher()
        {
            return View();
        }

    }
}