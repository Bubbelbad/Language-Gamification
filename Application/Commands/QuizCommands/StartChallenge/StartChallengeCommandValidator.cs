using FluentValidation;

namespace Application.Commands.QuizCommands.StartChallenge
{
    internal class StartChallengeCommandValidator : AbstractValidator<StartChallengeCommand>
    {
        public StartChallengeCommandValidator()
        {
            RuleFor(x => x.ChallengeId)
                .NotEmpty().GreaterThanOrEqualTo(1).WithMessage("ChallengeId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
