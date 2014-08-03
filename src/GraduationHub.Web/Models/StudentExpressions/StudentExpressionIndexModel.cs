using System;
using AutoMapper;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.StudentExpressions
{
    public class StudentExpressionIndexModel : IHaveCustomMappings
    {
        // Note: This is a datatables property for setting the Row Id.
        public int DT_RowId { get; set; }

        public string Student { get; set; }

        public StudentExpressionType Type { get; set; }

        public DateTime DateSubmitted { get; set; }

        public StudentExpressionType StudentExpressionType { get; set; }

        public string Text { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<StudentExpression, StudentExpressionIndexModel>()
                .ForMember(d=> d.Student, o=> o.MapFrom(s=> s.Student.LastName + ", " + s.Student.FirstName))
                .ForMember(d => d.DT_RowId, o => o.MapFrom(s => s.Id));

/*            configuration.CreateMap<StudentExpression, StudentExpressionIndexModel>()
                .ForMember(d => d.Student, o => o.MapFrom(s =>string.Format("{0}, {1}", s.Student.LastName, s.Student.FirstName)))
                .ForMember(d => d.DT_RowId, o => o.MapFrom(s => s.Id));*/
        }
    }
}