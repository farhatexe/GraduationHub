using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Models.GraduatingClass
{
    public class GraduatingClassCreateModel
    {
        [Required, StringLength(FieldLengths.GraduatingClass.Description)]
        public string Description { get; set; }
    }
}