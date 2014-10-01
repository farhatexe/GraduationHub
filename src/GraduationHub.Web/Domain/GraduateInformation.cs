using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    [Table("GraduateInformation")]
    public class GraduateInformation
    {
        public int Id { get; set; }

        [StringLength(FieldLengths.GraduateInformation.Name)]
        public string Name { get; set; }

        [StringLength(FieldLengths.Address.Street)]
        public string Street { get; set; }

        [StringLength(FieldLengths.Address.City)]
        public string City { get; set; }

        [StringLength(FieldLengths.GraduateInformation.StudentEmail)]
        public string StudentEmail { get; set; }

        [StringLength(FieldLengths.GraduateInformation.ParentEmail)]
        public string ParentEmail { get; set; }

        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual ApplicationUser Student { get; set; }

        public bool FineArts { get; set; }

        public bool AcademicClasses { get; set; }

        public bool WillParticipateInGraduation { get; set; }

        public bool? TakenKeysWorldView { get; set; }

        public bool? TakenApprovedWorldView { get; set; }

        public bool? WillSecureAnnouncements { get; set; }

        public bool? NeedCapAndGown { get; set; }

        [StringLength(FieldLengths.GraduateInformation.Height)]
        public string Height { get; set; }
    }
}