using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.CheckList
{
    public class InformationModel
    {
        [StringLength(FieldLengths.GraduateInformation.Name)]
        [Help("Full given name as you waould like it to appear on your diploma.")]
        public string Name { get; set; }

        [StringLength(FieldLengths.Address.Street)]
        [Display(Name = "Address")]
        public string AddressStreet { get; set; }

        [StringLength(FieldLengths.Address.City)]
        [Display(Name = "City")]
        public string AddressCity { get; set; }

        [StringLength(FieldLengths.Address.State)]
        [Display(Name = "State")]
        public string AddressState { get; set; }

        [StringLength(FieldLengths.Address.Zipcode)]
        [Display(Name = "Zip code")]
        public string AddressZipcode { get; set; }

        [StringLength(FieldLengths.GraduateInformation.StudentEmail)]
        [Display(Name = "Student's Email")]
        public string StudentEmail { get; set; }

        [StringLength(FieldLengths.GraduateInformation.ParentEmail)]
        [Display(Name = "Parent's Email")]
        public string ParentEmail { get; set; }

        public bool EnrolledFineArts { get; set; }

        public bool EnrolledAcademicClasses { get; set; }

        public bool WillParticipateInGraduation { get; set; }

        public bool TakenKeysWorldView { get; set; }

        public bool? TakenApprovedWorldView { get; set; }

        public bool WillSecureAnnouncements { get; set; }

        public bool NeedCapAndGown { get; set; }

        [StringLength(FieldLengths.GraduateInformation.Height)]
        public string Height { get; set; }
    }
}