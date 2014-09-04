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

        [ChildActionOnly]
        public ActionResult Login()
        {
            string userName = string.Empty;

            if (_currentUser.IsAuthenticated())
            {
                userName = _currentUser.User.FullName;
            }

            return PartialView("_LoginPartial", userName);
        }
    }
}