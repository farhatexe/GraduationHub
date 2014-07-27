using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.GraduatingClass
{
    public class GraduatingClassEditModel : IMapFrom<Domain.GraduatingClass>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required, StringLength(FieldLengths.GraduatingClass.Description)]
        public string Description { get; set; } 
    }
}