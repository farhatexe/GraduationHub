using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.ImportantDates
{
    public class ImportantDateEditModel : IMapFrom<ImportantDate>
    {
        [HiddenInput]
        public int Id { get; set; }
        
        [Display(Name = "Graduating Class")]
        public int GraduatingClassId { get; set; }

        public DateTime DueDate { get; set; }

        [Required, StringLength(FieldLengths.ImportantDate.Description), AllowHtml]
        public string Comments { get; set; }
    }
}