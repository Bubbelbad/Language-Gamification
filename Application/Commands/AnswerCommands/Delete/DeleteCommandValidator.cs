using FluentValidation;

namespace Application.Commands.AnswerCommands.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.AnswerId)
                .GreaterThan(0).WithMessage("Answer ID must be a positive number.");
        }
    }
}
