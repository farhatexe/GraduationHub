﻿using System;
using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.ImportantDates
{
    public class ImportantDateCreateModel : IMapFrom<ImportantDate>
    {
        [Display(Name = "Graduating Class")]
        public int GraduatingClassId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required, StringLength(FieldLengths.ImportantDate.Description)]
        public string Comments { get; set; }
    }
}