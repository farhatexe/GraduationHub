using System.Web.Mvc;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;

namespace GraduationHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoleService _roleService;

        public HomeController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [GraduationHubAuthorize(Roles = SecurityConstants.Roles.Student)]
        public ActionResult Index()
        {
            return View();
        }

        [GraduationHubAuthorize(Roles = SecurityConstants.Roles.Teacher)]
        public ActionResult Teacher()
        {
            return RedirectToAction("Index", "Invitations");
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {

            if (!_roleService.IsAuthenticated())
                return PartialView("_NavigationUnauthorized");

            return PartialView(_roleService.IsTeacher() ? "_NavigationTeacher" : "_NavigationStudent");
        }
    }
}