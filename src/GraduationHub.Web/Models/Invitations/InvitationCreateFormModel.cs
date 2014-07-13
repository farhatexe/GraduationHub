using System.ComponentModel.DataAnnotations;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationCreateFormModel
    {
        [Required]
        public string InviteeName { get; set; }

        [Required, Display(Name="Graduating Class")]
        public int GraduatingClassId { get; set; }

        [Required]
        public string Email { get; set; }

        public bool IsTeacher { get; set; }
    }
}