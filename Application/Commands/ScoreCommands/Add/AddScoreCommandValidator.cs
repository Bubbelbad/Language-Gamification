using FluentValidation;

namespace Application.Commands.ScoreCommands.Add
{
    public class AddScoreCommandValidator : AbstractValidator<AddScoreCommand>
    {
        public AddScoreCommandValidator()
        {
            RuleFor(x => x.Dto.UserId)
                .NotEmpty().NotNull().WithMessage("UserId is required");

            RuleFor(x => x.Dto.ChallengeId)
                .NotEmpty().GreaterThan(0).WithMessage("ChallengeId is required");

            RuleFor(x => x.Dto.CompletedAt)
                .NotEmpty().WithMessage("CompletedAt is required");

            RuleFor(x => x.Dto.Points)
                .NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Points is required");
        }
    }
}
