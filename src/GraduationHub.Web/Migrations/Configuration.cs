using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GraduationHub.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GraduationHub.Web.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GraduationHub.Web.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddUserAndRole(context);
        }

        private bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ir = rm.Create(new IdentityRole("Admin"));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser()
            {
                UserName = "gradhub@keysofva.org",
                FirstName = "System",
                LastName = "Admin"
            };

            ir = um.Create(user, "P@ssw0rd1");

            if (ir.Succeeded == false)
            {
                return ir.Succeeded;
            }

            ir = um.AddToRole(user.Id, "Admin");
            return ir.Succeeded;
        }
    }
}
