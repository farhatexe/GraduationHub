using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.ImportantDates
{
    public class ImportantDateCreateModel : IMapFrom<ImportantDate>
    {
        [Required]
        public DateTime DueDate { get; set; }

        [Required, StringLength(FieldLengths.ImportantDate.Description), AllowHtml]
        public string Comments { get; set; }
    }
}