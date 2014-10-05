using FluentValidation;
using GraduationHub.Web.Data;
using GraduationHub.Web.Models.CheckList;

namespace GraduationHub.Web.Validation
{
    public class InformationModelValidator : AbstractValidator<InformationModel>
    {
        public InformationModelValidator()
        {
            RuleFor(i => i.Name).NotEmpty().Length(1, FieldLengths.GraduateInformation.Name);
            RuleFor(i => i.Street).NotEmpty().Length(1, FieldLengths.Address.Street);
            RuleFor(i => i.City).NotEmpty().Length(1, FieldLengths.Address.City);
            RuleFor(i => i.StudentEmail)
                .NotEmpty()
                .EmailAddress()
                .Length(1, FieldLengths.GraduateInformation.StudentEmail);
            RuleFor(i => i.ParentEmail)
                .NotEmpty()
                .EmailAddress()
                .Length(1, FieldLengths.GraduateInformation.ParentEmail);
            RuleFor(i => i.WillParticipateInGraduation).NotNull().WithMessage(Messages.WillParticipateInGraduation); 
            RuleFor(i => i.TakenKeysWorldView)
                .NotNull()
                .When(i => i.WillParticipateInGraduation.HasValue && i.WillParticipateInGraduation.Value);
            RuleFor(i => i.TakenApprovedWorldView)
                .NotNull()
                .When(i => i.TakenKeysWorldView.HasValue && !i.TakenKeysWorldView.Value)
                .WithMessage(Messages.TakenApprovedWorldView); 
            RuleFor(i => i.WillSecureAnnouncements)
                .NotNull()
                .When(i => i.WillParticipateInGraduation.HasValue && i.WillParticipateInGraduation.Value)
                .WithMessage(Messages.WillParticipateInGraduation); 
            RuleFor(i => i.NeedCapAndGown)
                .NotNull()
                .When(i => i.WillParticipateInGraduation.HasValue && i.WillParticipateInGraduation.Value)
                .WithMessage(Messages.WillParticipateInGraduation); 

            RuleFor(i => i.Height)
                .NotNull()
                .When(i => i.WillParticipateInGraduation.HasValue && i.WillParticipateInGraduation.Value)
                .WithMessage(Messages.WillParticipateInGraduation)
                       .Length(1, FieldLengths.GraduateInformation.Height);
        }

        public class Messages
        {
            public const string RequiredIfGraduating = "Answer required if you are participating in the KEYS Graduation ceremony.'";

            public const string WillParticipateInGraduation =
                "We need to know if you are participating in the KEYS Graduation ceremony.";

            public const string TakenApprovedWorldView =
                "Answer required if you have not taken Worldview Analysis at KEYS.";

        }
    }
}