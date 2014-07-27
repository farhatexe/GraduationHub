using AutoMapper;
using GraduationHub.Web.Infrastructure.Mapping;


namespace GraduationHub.Web.Models.GraduatingClass
{
    public class GraduatingClassIndexModel : IHaveCustomMappings
    {
        // Note: This is a datatables property for setting the Row Id.
        public int DT_RowId { get; set; }

        public string Description { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Domain.GraduatingClass, GraduatingClassIndexModel>()
            .ForMember(d => d.DT_RowId, o => o.MapFrom(s => s.Id));
        }
    }
}