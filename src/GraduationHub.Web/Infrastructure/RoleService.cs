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


        public RoleService(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public bool IsAuthenticated()
        {
            return _currentUser.User != null;
        }

        public bool IsInRole(string role)
        {
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
    }
}