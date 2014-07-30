using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    public class FrequentlyAskedQuestion
    {
        public int Id { get; set; }

        
        public int Number { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Question)]
        public string Question { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Answer)]
        public string Answer { get; set; }
    }
}