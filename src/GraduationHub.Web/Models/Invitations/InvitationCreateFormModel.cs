using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationCreateFormModel
    {
        [Required, StringLength(FieldLengths.Invitation.InviteeName)]
        public string InviteeName { get; set; }

        [Display(Name = "Graduating Class"), Help("If the Invitee is a Teacher, select 'None'.")]
        public int GraduatingClassId { get; set; }

        [Required, StringLength(FieldLengths.Invitation.Email)]
        public string Email { get; set; }

        public bool IsTeacher { get; set; }
    }
}