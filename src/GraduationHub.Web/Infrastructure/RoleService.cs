using System.Web;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GraduationHub.Web.Infrastructure
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly HttpContextBase _httpContext;


        public RoleService(ApplicationDbContext context, ICurrentUser currentUser, HttpContextBase httpContext)
        {
            _context = context;
            _currentUser = currentUser;
            _httpContext = httpContext;
        }

        public bool IsAuthenticated()
        {
            return _httpContext.Request.IsAuthenticated && _currentUser.User !=null;
        }

        public bool IsInRole(string role)
        {
            if (!IsAuthenticated())
            {
                return false;
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            return userManager.IsInRole(_currentUser.User.Id, role);
        }

        public bool IsTeacher()
        {
            return IsInRole(SecurityConstants.Roles.Teacher);
        }

        public bool IsStudent()
        {
            return IsInRole(SecurityConstants.Roles.Student);
        }

        public bool IsAdmin()
        {
            return IsInRole(SecurityConstants.Roles.Admin);
        }
    }
}