using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationCreateFormModel
    {
        [Required]
        public string InviteeName { get; set; }

        [Required, Display(Name="Graduating Class"), Help("If the Invitee is a Teacher, select 'None'.")]
        public int GraduatingClassId { get; set; }

        [Required]
        public string Email { get; set; }

        public bool IsTeacher { get; set; }
    }
}