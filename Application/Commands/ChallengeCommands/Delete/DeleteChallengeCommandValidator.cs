using FluentValidation;


namespace Application.Commands.ChallengeCommands.Delete
{
    public sealed class DeleteChallengeCommandValidator : AbstractValidator<DeleteChallengeCommand>
    {
        public DeleteChallengeCommandValidator()
        {
            RuleFor(x => x.ChallengeId)
                .NotNull().WithMessage("Id is required.");
        }
    }
}
