using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.FrequentlyAskedQuestions
{
    public class FrequentlyAskedQuestionIndexModel : IHaveCustomMappings
    {
        // Note: This is a datatables property for setting the Row Id.
        public int DT_RowId { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Question)]
        public string Question { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Answer)]
        public string Answer { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FrequentlyAskedQuestion, FrequentlyAskedQuestionIndexModel>()
                .ForMember(d => d.DT_RowId, o => o.MapFrom(s => s.Id));
        }
    }
}