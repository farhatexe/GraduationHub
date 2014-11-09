using System.Data.Entity.ModelConfiguration.Configuration;

namespace GraduationHub.Web.Infrastructure
{
    public interface IEntityConfiguration
    {
        void AddConfiguration(ConfigurationRegistrar registrar);

    }
}