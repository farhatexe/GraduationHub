using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.ImportantDates
{
    public class ImportantDateEditModel : IMapFrom<ImportantDate>
    {
        [HiddenInput]
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public string GraduatingClassId { get; set; }

        public string Description { get; set; }
    }
}