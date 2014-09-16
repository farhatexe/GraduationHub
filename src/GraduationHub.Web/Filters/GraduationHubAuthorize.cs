using System.Web.Mvc;
using System.Web.Routing;
using GraduationHub.Web.Infrastructure;

namespace GraduationHub.Web.Filters
{
    public class GraduationHubAuthorize : AuthorizeAttribute
    {
        public IRoleService RoleService { get; set; }

        /// <summary>
        ///     If the User is a Student, Redirect UnAuth to Student Home.
        ///     If the User is a Teacher, Redirect UnAuth to Teacher Home.
        ///     If Neither, redirect to default.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (RoleService == null || !RoleService.IsAuthenticated())
            {
                base.HandleUnauthorizedRequest(filterContext);
                return;
            }

            if (RoleService.IsTeacher() || RoleService.IsAdmin())
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Home",
                            action = "Teacher"
                        }));
            }
            else if (RoleService.IsStudent())
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            area = "",
                            controller = "Home",
                            action = "Index"
                        }));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }

        }
    }
}