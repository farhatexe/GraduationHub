using AutoMapper;

namespace GraduationHub.Web.Infrastructure.Mapping
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(IConfiguration configuration);
	}
}