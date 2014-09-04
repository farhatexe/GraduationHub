using System.Web.Mvc;
using System.Web.Routing;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GraduationHub.Web.Filters
{
    public class GraduationHubAuthorize : AuthorizeAttribute
    {
        public ApplicationDbContext Context { get; set; }
        public ICurrentUser CurrentUser { get; set; }


        /// <summary>
        ///     If the User is a Student, Redirect UnAuth to Student Home.
        ///     If the User is a Teacher, Redirect UnAuth to Teacher Home.
        ///     If Neither, redirect to default.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));

            bool isTeacher = um.IsInRole(CurrentUser.User.Id, SecurityConstants.Roles.Teacher);
            bool isStudent = um.IsInRole(CurrentUser.User.Id, SecurityConstants.Roles.Student);

            if (isTeacher)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Home",
                            action = "Teacher"
                        }));
            }
            else if (isStudent)
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