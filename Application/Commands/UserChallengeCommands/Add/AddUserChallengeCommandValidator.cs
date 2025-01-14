using FluentValidation;

namespace Application.Commands.UserChallengeCommands.Add
{
    public class AddUserChallengeCommandValidator : AbstractValidator<AddUserChallengeCommand>
    {
        public AddUserChallengeCommandValidator()
        {
            RuleFor(x => x.Dto.UserId)
                 .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Dto.ChallengeId)
                .NotEmpty().WithMessage("ChallengeId is required.");

            RuleFor(x => x.Dto.Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score must be a non-negative number.")
                .NotEmpty().WithMessage("Score is required.");

            RuleFor(x => x.Dto.CompletedAt)
                .NotNull().WithMessage("CompletedAt is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CompletedAt cannot be a future date.");
        }
    }
}
