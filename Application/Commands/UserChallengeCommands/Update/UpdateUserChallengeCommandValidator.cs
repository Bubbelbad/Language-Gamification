using FluentValidation;

namespace Application.Commands.UserChallengeCommands.Update
{
    public class UpdateUserChallengeCommandValidator : AbstractValidator<UpdateUserChallengeCommand>
    {
        public UpdateUserChallengeCommandValidator()
        {
            RuleFor(x => x.Dto.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Dto.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Dto.ChallengeId)
                .GreaterThan(0).WithMessage("ChallengeId must be greater than 0.");

            RuleFor(x => x.Dto.Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score must be a non-negative number.");

            RuleFor(x => x.Dto.CompletedAt)
                .NotNull().WithMessage("CompletedAt is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CompletedAt cannot be a future date.");
        }
    }
}
