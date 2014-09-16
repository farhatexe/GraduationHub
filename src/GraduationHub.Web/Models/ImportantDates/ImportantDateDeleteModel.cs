using System;
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

        public string Comments { get; set; }
    }
}