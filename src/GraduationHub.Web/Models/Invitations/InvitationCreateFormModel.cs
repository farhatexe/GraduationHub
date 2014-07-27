using System.ComponentModel.DataAnnotations;
using FluentValidation;
using GraduationHub.Web.Data;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.Invitations
{
    public class InvitationCreateFormModel
    {
        public string InviteeName { get; set; }

        [Display(Name="Graduating Class"), Help("If the Invitee is a Teacher, select 'None'.")]
        public int GraduatingClassId { get; set; }

        public string Email { get; set; }

        public bool IsTeacher { get; set; }
    }

    public class InvitationCreateFormModelValidator : AbstractValidator<InvitationCreateFormModel>
    {
        public InvitationCreateFormModelValidator()
        {
            RuleFor(x => x.InviteeName).NotNull().Length(FieldLengths.Invitation.InviteeName);

            RuleFor(x => x.GraduatingClassId).GreaterThan(0);

            RuleFor(x => x.Email).NotNull().Length(FieldLengths.Invitation.Email);
        }
    }
}