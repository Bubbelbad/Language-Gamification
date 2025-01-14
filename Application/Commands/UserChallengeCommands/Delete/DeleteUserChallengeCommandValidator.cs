using FluentValidation;

namespace Application.Commands.UserChallengeCommands.Delete
{
    public class DeleteUSerChallengeCommandValidator : AbstractValidator<DeleteUserChallengeCommand>
    {
        public DeleteUSerChallengeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.");
        }
    }
}
