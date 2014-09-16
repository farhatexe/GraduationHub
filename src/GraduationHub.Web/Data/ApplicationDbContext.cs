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

        public DbSet<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; }

        public DbSet<GraduateInformation> GraduateInformation { get; set; }

        public DbSet<ImportantDate> ImportantDates { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<StudentExpression> StudentExpressions { get; set; }

        public DbSet<StudentPicture> StudentPictures { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}