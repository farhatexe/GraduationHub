using System.Security.Principal;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using Microsoft.AspNet.Identity;

namespace GraduationHub.Web.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentity _identity;

        private ApplicationUser _user;

        public CurrentUser(IIdentity identity, ApplicationDbContext context)
        {
            _identity = identity;
            _context = context;
        }

        public ApplicationUser User
        {
            get { return _user ?? (_user = _context.Users.Find(_identity.GetUserId())); }
        }
    }
}