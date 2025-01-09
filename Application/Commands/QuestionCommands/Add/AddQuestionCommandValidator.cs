using FluentValidation;

namespace Application.Commands.QuestionCommands.Add
{
    public class AddQuestionCommandValidator : AbstractValidator<AddQuestionCommand>
    {
        public AddQuestionCommandValidator()
        {
            RuleFor(x => x.Dto.Text)
                .NotNull().WithMessage("Text is required.")
                .NotEmpty().WithMessage("Text is required.");

            RuleFor(x => x.Dto.CorrectAnswerId)
                .NotNull().WithMessage("CorrectAnswerId is required.")
                .NotEmpty().WithMessage("CorrectAnswerId is required.");
        }
    }
}
