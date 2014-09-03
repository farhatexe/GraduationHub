using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.FrequentlyAskedQuestions
{
    public class FrequentlyAskedQuestionDeleteModel : IMapFrom<FrequentlyAskedQuestion>
    {
        [HiddenInput]
        public int Id { get; set; }

        public int Order { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Question),
            DataType(DataType.MultilineText)]
        public string Question { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Answer),
            DataType(DataType.MultilineText)]
        public string Answer { get; set; } 
    }
}