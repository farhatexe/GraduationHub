﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Domain;

namespace GraduationHub.Web.Models.StudentExpressions
{
    public class StudentExpressionCreateModel
    {
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [HiddenInput]
        public int TextMaxLength { get; set; }
    }
}