using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Models.Information
{
    public class InformationIndexViewModel
    {
        [StringLength(FieldLengths.GraduateInformation.Name), Required]
        public string Name { get; set; }

        [StringLength(FieldLengths.Address.Street), Required]
        public string AddressStreet { get; set; }

        [StringLength(FieldLengths.Address.City), Required]
        public string AddressCity { get; set; }

        [StringLength(FieldLengths.Address.State), Required]
        public string AddressState { get; set; }

        [StringLength(FieldLengths.Address.Zipcode), Required]
        public string AddressZipCode { get; set; }

        [StringLength(FieldLengths.GraduateInformation.StudentEmail), Required]
        [EmailAddress]
        public string StudentEmail { get; set; }

        [StringLength(FieldLengths.GraduateInformation.ParentEmail), Required]
        [EmailAddress]
        public string ParentEmail { get; set; }

        [Required]
        public bool EnrolledFineArts { get; set; }

        [Required]
        public bool EnrolledAcademicClasses { get; set; }

        [Required]
        public bool WillParticipateInGraduation { get; set; }

        [Required]
        public bool TakenKeysWorldView { get; set; }

        [Required]
        public bool? TakenApprovedWorldView { get; set; }

        [Required]
        public bool WillSecureAnnouncements { get; set; }

        [Required]
        public bool NeedCapAndGown { get; set; }

        [StringLength(FieldLengths.GraduateInformation.Height)]
        public string Height { get; set; }
    }
}