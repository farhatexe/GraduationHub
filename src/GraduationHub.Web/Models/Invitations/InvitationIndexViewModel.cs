using AutoMapper;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationIndexViewModel : IHaveCustomMappings
    {
        // Note: This is a datatables property for setting the Row Id.
        public int DT_RowId { get; set; }

        public string StudentName { get; set; }

        public string Email { get; set; }

        public string GraduatingClassDescription { get; set; }

        public bool HasBeenRedeemed { get; set; }

        public bool HasBeenSent { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Invitation, InvitationIndexViewModel>()
                .ForMember(d => d.DT_RowId, o => o.MapFrom(s => s.Id));
        }
    }
}