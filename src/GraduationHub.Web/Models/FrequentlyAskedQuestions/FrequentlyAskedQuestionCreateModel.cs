using System;
using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Models.FrequentlyAskedQuestions
{
    public class FrequentlyAskedQuestionCreateModel
    {
        [Range(1, Int32.MaxValue)]
        public int Number { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Question),
            DataType(DataType.MultilineText)]
        public string Question { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Answer),
            DataType(DataType.MultilineText)]
        public string Answer { get; set; } 
    }
}