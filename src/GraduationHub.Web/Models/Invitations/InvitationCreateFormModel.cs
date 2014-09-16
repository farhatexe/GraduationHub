using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationCreateFormModel
    {
        [Required, StringLength(FieldLengths.Invitation.InviteeName)]
        public string InviteeName { get; set; }

        [Required, StringLength(FieldLengths.Invitation.Email)]
        public string Email { get; set; }

        public bool IsTeacher { get; set; }
    }
}