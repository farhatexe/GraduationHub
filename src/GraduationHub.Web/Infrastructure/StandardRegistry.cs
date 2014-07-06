using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace GraduationHub.Web.Infrastructure
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}