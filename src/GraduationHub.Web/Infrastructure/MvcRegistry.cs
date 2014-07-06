using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace GraduationHub.Web.Infrastructure
{
    public class MvcRegistry : Registry
    {
        public MvcRegistry(Func<IContainer> containerFactory)
        {
            For<BundleCollection>().Use(BundleTable.Bundles);
            For<RouteCollection>().Use(RouteTable.Routes);
            For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
            For<HttpSessionStateBase>()
                .Use(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
            For<HttpContextBase>()
                .Use(() => new HttpContextWrapper(HttpContext.Current));
            For<HttpServerUtilityBase>()
                .Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));

            For<IUserStore<ApplicationUser>>()
                .Use<UserStore<ApplicationUser>>();

            /*This is a factory to get the DBContext for the Identity*/
            For<DbContext>().Use(() => new ApplicationDbContext());
        }
    }
}