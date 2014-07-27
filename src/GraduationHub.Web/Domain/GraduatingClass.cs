using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    public class GraduatingClass
    {
        public int Id { get; set; }

        [StringLength(FieldLengths.GraduatingClass.Description)]
        public string Description { get; set; }
    }
}