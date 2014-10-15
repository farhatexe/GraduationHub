using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GraduationHub.Web.Infrastructure;

namespace GraduationHub.Web.Filters
{
    public class GraduationHubAuthorize : AuthorizeAttribute
    {
        public IRoleService RoleService { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            string[] roleSplit = SplitString(Roles);

            if (roleSplit.Length > 0 && !roleSplit.Any(RoleService.IsInRole))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     If the User is a Student, Redirect UnAuth to Student Home.
        ///     If the User is a Teacher, Redirect UnAuth to Teacher Home.
        ///     If Neither, redirect to default.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (RoleService == null)
            {
                // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (RoleService.IsTeacher() || RoleService.IsAdmin())
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            area = "",
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
                // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }


        internal static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            IEnumerable<string> split = from piece in original.Split(',')
                let trimmed = piece.Trim()
                where !String.IsNullOrEmpty(trimmed)
                select trimmed;
            return split.ToArray();
        }
    }
}