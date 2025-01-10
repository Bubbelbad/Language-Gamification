using FluentValidation;

namespace Application.Commands.AnswerCommands.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator() 
        {
            RuleFor(x => x.NewAnswer.Text)
                .NotEmpty().WithMessage("Text is required")
                .NotNull().WithMessage("Text is required");

            RuleFor(x => x.NewAnswer.QuestionId)
                .NotEmpty().WithMessage("QuestionId is required")
                .NotNull().WithMessage("QuestionId is required");

            RuleFor(x => x.NewAnswer.IsCorrect)
                .NotEmpty().WithMessage("IsCorrect is required")
                .NotNull().WithMessage("IsCorrect is required");
        }
    }
}
