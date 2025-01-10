using FluentValidation;

namespace Application.Commands.AnswerCommands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator() 
        {
            RuleFor(x => x.NewAnswer.Id)
                .NotNull().WithMessage("AnswerId is required.");

            RuleFor(x => x.NewAnswer.Text)
                .NotEmpty().WithMessage("Text is required.")
                .Length(3, 100).WithMessage("Text must be between 3 and 100 characters.");

            RuleFor(x => x.NewAnswer.QuestionId)
                .NotNull().WithMessage("QuestionId is required.");

            RuleFor(x => x.NewAnswer.IsCorrect)
                .NotNull().WithMessage("IsCorrect is required.");
        }
    }
}
