using FluentValidation;

namespace Application.Commands.QuestionCommands.Delete
{
    public sealed class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.");
        }
    }
}
