using System.ComponentModel.DataAnnotations;

namespace GraduationHub.Web.Domain
{
    public class GraduatingClass
    {
        public int Id { get; set; }

        [StringLength(75)]
        public string Description { get; set; }
    }
}