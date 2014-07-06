using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GraduationHub.Web.Startup))]
namespace GraduationHub.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
