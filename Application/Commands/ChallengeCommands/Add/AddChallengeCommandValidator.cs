using FluentValidation;

namespace Application.Commands.ChallengeCommands.Add
{
    public class AddChallengeCommandValidator : AbstractValidator<AddChallengeCommand>
    {
        public AddChallengeCommandValidator() 
        {
            RuleFor(x => x.Dto)
                .NotNull().WithMessage("Challenge data is required");

            RuleFor(x => x.Dto.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title must not exceed 100 characters.");

        }
    }
}
