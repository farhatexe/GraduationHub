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

        public Address Address { get; set; }

        [StringLength(FieldLengths.GraduateInformation.StudentEmail)]
        public string StudentEmail { get; set; }

        [StringLength(FieldLengths.GraduateInformation.ParentEmail)]
        public string ParentEmail { get; set; }

        public bool EnrolledFineArts { get; set; }

        public bool EnrolledAcademicClasses { get; set; }

        public bool WillParticiateInGraduation { get; set; }

        public bool TakenKeysWorldView { get; set; }

        public bool? TakenApprovedWorldView { get; set; }

        public bool WillSecureAnnouncements { get; set; }

        public bool NeedCapAndGown { get; set; }

        [StringLength(FieldLengths.GraduateInformation.Height)]
        public string Height { get; set; }
    }
}