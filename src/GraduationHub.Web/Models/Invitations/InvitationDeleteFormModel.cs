using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationDeleteFormModel : IMapFrom<Invitation>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string InviteeName { get; set; }

        [Display(Name="Graduating Class")]
        public string GraduatingClassDescription { get; set; }

        public string Email { get; set; }

        public bool HasBeenRedeemed { get; set; }

        public bool HasBeenSent { get; set; }
    }
}