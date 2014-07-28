using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.ImportantDates
{
    public class ImportantDateDeleteModel : IMapFrom<ImportantDate>
    {
        [HiddenInput]
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        [Display(Name = "Graduating Class")]
        public string GraduatingClassDescription { get; set; }

        public string Description { get; set; }
    }
}