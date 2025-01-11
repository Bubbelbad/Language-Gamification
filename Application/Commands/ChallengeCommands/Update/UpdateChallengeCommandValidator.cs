using FluentValidation;

namespace Application.Commands.ChallengeCommands.Update
{
    public class UpdateChallengeCommandValidator : AbstractValidator<UpdateChallengeCommand>
    {
        public UpdateChallengeCommandValidator()
        {
            RuleFor(x => x.Dto.Id)
                .NotNull().WithMessage("Id is required.");

            RuleFor(x => x.Dto.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(x => x.Dto.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
