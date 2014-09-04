using System;
using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.CheckList 
{
    public class CheckListImportantDate : IMapFrom<ImportantDate>
    {
        public DateTime DueDate { get; set; }

        [AllowHtml]
        public string Comments { get; set; }
    }
}