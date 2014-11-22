using System;
using AutoMapper;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Students
{
    [Obsolete]
    public class StudentInformationModel : IHaveCustomMappings
    {
        public string StudentName { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string StudentEmail { get; set; }
        public string ParentEmail { get; set; }
        public string StudentId { get; set; }
        public bool FineArts { get; set; }
        public bool AcademicClasses { get; set; }
        public bool WillParticipateInGraduation { get; set; }
        public bool? TakenKeysWorldView { get; set; }
        public bool? TakenApprovedWorldView { get; set; }
        public bool? WillSecureAnnouncements { get; set; }
        public bool? NeedCapAndGown { get; set; }
        public string Height { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Domain.GraduateInformation, StudentInformationModel>()
                .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student.FirstName + " " + s.Student.LastName))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Street + ", " + s.City));
        }
    }
}