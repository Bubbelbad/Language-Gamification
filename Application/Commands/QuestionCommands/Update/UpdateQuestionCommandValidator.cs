using FluentValidation;

namespace Application.Commands.QuestionCommands.Update
{
    public sealed class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(x => x.Dto.Id)
                .NotNull().WithMessage("Id is required.")
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Dto.Text)
                .NotNull().WithMessage("Text is required.")
                .NotEmpty().WithMessage("Text is required.");

            RuleFor(x => x.Dto.CorrectAnswerId)
                .NotNull().WithMessage("CorrectAnswerId is required.")
                .NotEmpty().WithMessage("CorrectAnswerId is required.");
        }
    }
}
