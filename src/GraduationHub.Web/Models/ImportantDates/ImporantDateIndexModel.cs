using System;
using AutoMapper;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.ImportantDates
{
    public class ImportantDateIndexModel : IHaveCustomMappings
    {
        // Note: This is a datatables property for setting the Row Id.
        public int DT_RowId { get; set; }

        public DateTime DueDate { get; set; }

        public string Comments { get; set; }
        
        public string GraduatingClassDescription { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ImportantDate, ImportantDateIndexModel>()
                .ForMember(d => d.DT_RowId, o => o.MapFrom(s => s.Id));
        }
    }
}