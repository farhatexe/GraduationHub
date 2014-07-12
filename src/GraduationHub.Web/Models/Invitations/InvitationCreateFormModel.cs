using System.ComponentModel.DataAnnotations;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationCreateFormModel
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        public int GraduatingClassId { get; set; }

        [Required]
        public string Email { get; set; }
    }
}