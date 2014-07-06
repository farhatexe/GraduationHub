using System.Data.Entity;
using GraduationHub.Web.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GraduationHub.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public DbSet<LogAction> Logs { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}