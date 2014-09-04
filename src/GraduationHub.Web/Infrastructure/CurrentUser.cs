using System.Security.Principal;
using System.Web;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using Microsoft.AspNet.Identity;

namespace GraduationHub.Web.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextBase _httpContext;
        private readonly IIdentity _identity;

        private ApplicationUser _user;

        public CurrentUser(IIdentity identity, ApplicationDbContext context, HttpContextBase httpContext)
        {
            _identity = identity;
            _context = context;
            _httpContext = httpContext;
        }

        public ApplicationUser User
        {
            get { return _user ?? (_user = _context.Users.Find(_identity.GetUserId())); }
        }

        public bool IsAuthenticated()
        {
            return _httpContext.Request.IsAuthenticated;
        }
    }
}