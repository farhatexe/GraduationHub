using System.Web.Mvc;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;

namespace GraduationHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ICurrentUser _currentUser;

        public HomeController(IRoleService roleService, ICurrentUser currentUser)
        {
            _roleService = roleService;
            _currentUser = currentUser;
        }

        [GraduationHubAuthorize(Roles = SecurityConstants.Roles.Student)]
        public ActionResult Index()
        {
            return View();
        }

       [GraduationHubAuthorize(Roles = "Teacher, Admin")]
        public ActionResult Teacher()
        {
            return RedirectToAction("Index", "Invitations");
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {

            if (!_roleService.IsAuthenticated())
                return PartialView("_NavigationUnauthorized");

            // NOTE: Because we are in a Child View "Action" from the View's POV will be "Menu"
            var rd = ControllerContext.ParentActionViewContext.RouteData;
            ViewBag.CurrentAction = rd.GetRequiredString("action");
            ViewBag.CurrentController = rd.GetRequiredString("controller");

            return PartialView(_roleService.IsTeacher() || _roleService.IsAdmin() ? "_NavigationTeacher" : "_NavigationStudent");
        }

        [ChildActionOnly]
        public ActionResult Login()
        {
            string userName = string.Empty;

            if (_roleService.IsAuthenticated())
            {
                userName = _currentUser.User.FullName;
            }

            return PartialView("_LoginPartial", userName);
        }
    }
}