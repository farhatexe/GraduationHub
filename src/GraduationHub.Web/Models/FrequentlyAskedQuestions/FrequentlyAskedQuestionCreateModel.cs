﻿using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Models.FrequentlyAskedQuestions
{
    public class FrequentlyAskedQuestionCreateModel
    {
        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Question)]
        public string Question { get; set; }

        [Required, StringLength(FieldLengths.FrequentlyAskedQuestion.Answer),
            DataType(DataType.MultilineText)]
        public string Answer { get; set; } 
    }
}