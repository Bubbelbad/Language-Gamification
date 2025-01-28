using FluentValidation;

namespace Application.Commands.QuizCommands.SubmitAnswer
{
    public class SubmitAnswerCommandValidator : AbstractValidator<SubmitAnswerCommand>
    {
        public SubmitAnswerCommandValidator()
        {
            RuleFor(x => x.UserChallengeId)
                .GreaterThan(0)
                .WithMessage("User challenge ID must be greater than 0.");

            RuleFor(x => x.AnswerId)
                .GreaterThan(0)
                .WithMessage("Answer ID must be greater than 0.");
        }
    }
}
