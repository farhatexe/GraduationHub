using System.Data.Entity;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GraduationHub.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<LogAction> Logs { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}