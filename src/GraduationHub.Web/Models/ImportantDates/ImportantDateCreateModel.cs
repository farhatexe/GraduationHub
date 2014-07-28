using System;
using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.ImportantDates
{
    public class ImportantDateCreateModel : IMapFrom<ImportantDate>
    {
        [Required]
        public DateTime DueDate { get; set; }

        [Required, StringLength(FieldLengths.ImportantDate.Description)]
        public string Description { get; set; }

        [Display(Name = "Graduating Class"), Help("If the Invitee is a Teacher, select 'None'.")]
        public int GraduatingClassId { get; set; }
    }
}