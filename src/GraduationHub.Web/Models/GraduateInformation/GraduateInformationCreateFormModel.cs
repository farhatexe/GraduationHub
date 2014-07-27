using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.GraduateInformation
{
    public class GraduateInformationCreateFormModel
    {
        [Required, StringLength(75)]
        [Help("Full given name as you would like it to appear on your diploma")]
        public string Name { get; set; }

        [StringLength(75), Display(Name = "Address")]
        public string AddressStreet { get; set; }

        [StringLength(75), Display(Name = "City")]
        public string AddressCity { get; set; }

        [StringLength(2), Display(Name = "State")]
        public string AddressState { get; set; }

        [StringLength(10), Display(Name = "Zip code")]
        public string AddressZipcode { get; set; } 

        [Display(Name = "Student's Email")]
        public string StudentEmail { get; set; }

        [Display(Name = "Parent's Email")]
        public string ParentEmail { get; set; }

        [Help("During my studies at KEYS, I have enrolled in Fine Arts Classes"), DataType("YesNo")]
        public bool FineArtsEnrollment { get; set; }

        [Help("During my studies at KEYS, I have enrolled in Academic Classes"), DataType("YesNo")]
        public bool AcademicClassEnrollment { get; set; }

        [Help("I will participate in the KEYS Graduation ceremony"), DataType("YesNo")]
        public bool GraduationParticipant { get; set; }

        [Help("I have taken or am currently taking Worldview Analysis at KEYS"), DataType("YesNo")]
        public bool WorldViewAnalysis { get; set; }

        [Help(
            
                "If you selected NO, do you have a letter on file with the KEYS office that you are taking a Worldview class at home that has been approved by KEYS?"
            ), DataType("YesNo")]
        public bool? OtherApprovedWorldViewAnalysis { get; set; }

        [Help(
            "I will secure my own graduation announcements. KEYS will have a PDF of a simple announcement available at your request if you do not want to secure your own. Please contact the KEYS office if you would like it emailed to you."
            ), DataType("YesNo")]
        public bool WillSecureAnnouncements { get; set; }

        [Help("Do you need a black cap and gown?"), DataType("YesNo")]
        public bool NeedCapAndGown { get; set; }

        [Help("If you need a cap and gown, what is your height with shoes? (please measure according to the type of shoe you would wear with your gown in feet and inches)")]
        public string Height { get; set; }
    }
}