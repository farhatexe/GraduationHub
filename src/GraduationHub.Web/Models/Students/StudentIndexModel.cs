using System.Linq;
using AutoMapper;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Students
{
    public class StudentIndexModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string StudentName { get; set; }

        public bool HasBiography { get; set; }

        public bool HasThanks { get; set; }

        public bool HasSlide { get; set; }

        public bool HasSeniorPortrait { get; set; }

        public bool HasBabyPicture { get; set; }

        public bool HasToddlerPicture { get; set; }

        public bool HasElementarySchoolPicture { get; set; }

        public bool HasMiddleSchoolPicture { get; set; }

        public bool HasHighSchoolPicture { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, StudentIndexModel>()
                .ForMember(m => m.StudentName, o => o.MapFrom(s => s.LastName + ", " + s.FirstName))
                .ForMember(m => m.HasBiography,
                    o => o.MapFrom(s => s.StudentExpressions.Any(p => p.Type == StudentExpressionType.Biography)))
                .ForMember(m => m.HasThanks,
                    o => o.MapFrom(s => s.StudentExpressions.Any(p => p.Type == StudentExpressionType.ThankYou)))
                .ForMember(m => m.HasSlide,
                    o =>
                        o.MapFrom(
                            s => s.StudentExpressions.Any(p => p.Type == StudentExpressionType.SlideshowCaption)))
                .ForMember(m => m.HasSeniorPortrait,
                    o =>
                        o.MapFrom(s => s.StudentPictures.Any(p => p.ImageType == StudentPictureType.SeniorPortrait)))
                .ForMember(m => m.HasBabyPicture,
                    o => o.MapFrom(s => s.StudentPictures.Any(p => p.ImageType == StudentPictureType.BabyPicture)))
                .ForMember(m => m.HasToddlerPicture,
                    o =>
                        o.MapFrom(s => s.StudentPictures.Any(p => p.ImageType == StudentPictureType.ToddlerPicture)))
                .ForMember(m => m.HasElementarySchoolPicture,
                    o =>
                        o.MapFrom(
                            s => s.StudentPictures.Any(p => p.ImageType == StudentPictureType.ElementaryPicture)))
                .ForMember(m => m.HasMiddleSchoolPicture,
                    o =>
                        o.MapFrom(
                            s => s.StudentPictures.Any(p => p.ImageType == StudentPictureType.MiddleSchoolPicture)))
                .ForMember(m => m.HasHighSchoolPicture,
                    o =>
                        o.MapFrom(
                            s => s.StudentPictures.Any(p => p.ImageType == StudentPictureType.HighSchoolPicture)))
                ;
        }
    }
}