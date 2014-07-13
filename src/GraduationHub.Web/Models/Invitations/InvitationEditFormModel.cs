using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationEditFormModel : IMapFrom<Invitation>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string InviteeName { get; set; }

        [Required]
        public int GraduatingClassId { get; set; }

        [Required]
        public string Email { get; set; }

        public bool HasBeenRedeemed { get; set; }

        public bool IsTeacher { get; set; }
    }
}