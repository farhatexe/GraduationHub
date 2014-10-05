using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;
using GraduationHub.Web.Infrastructure.ModelMetadata;
using GraduationHub.Web.Validation;

namespace GraduationHub.Web.Models.CheckList
{

    [Validator(typeof(InformationModelValidator))]
    public class InformationModel : IMapFrom<GraduateInformation>
    {
        [Display(Name = "Full Given Name"), Watermark(PlaceHolder = "As you would like it to appear on your diploma.")]
        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        [Display(Name = "Student's Email")]
        public string StudentEmail { get; set; }

        [Display(Name = "Parent's Email")]
        public string ParentEmail { get; set; }

        public bool FineArts { get; set; }

        public bool AcademicClasses { get; set; }

        [DataType("YesNo"), Display(Name = "Will you participate in the KEYS Graduation ceremony?")]
        public bool? WillParticipateInGraduation { get; set; }

        [DataType("YesNo"), Display(Name = "I have taken or am currently taking Worldview Analysis at KEYS:")]
        public bool? TakenKeysWorldView { get; set; }

        [DataType("YesNo"), Display(
            Name =
                "If you selected NO, do you have a letter on file with the KEYS office that you are taking a Worldview class at home that has been approved by KEYS?"
            )]
        public bool? TakenApprovedWorldView { get; set; }

        [DataType("YesNo"), Display(Name = "I will secure my own graduation announcements:")]
        public bool? WillSecureAnnouncements { get; set; }

        [DataType("YesNo"), Display(Name = "Do you need a black cap and gown?")]
        public bool? NeedCapAndGown { get; set; }

        [Display(Name = "What is your height with *shoes?")]
        [Watermark(PlaceHolder = "Height Example: 5' 6\"")]
        public string Height { get; set; }
    }
}