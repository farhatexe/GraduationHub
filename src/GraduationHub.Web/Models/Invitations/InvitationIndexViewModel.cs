using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationIndexViewModel : IMapFrom<Invitation>
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string Email { get; set; }

        public string GraduatingClassDescription { get; set; }

        public bool HasBeenRedeemed { get; set; }

        public bool HasBeenSent { get; set; }
    }
}