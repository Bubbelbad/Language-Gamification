using FluentValidation;

namespace Application.Commands.UserCommands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.NewUser.UserName)
                .NotEmpty().WithMessage("Username is required")
                .NotNull().WithMessage("Username is required");

            RuleFor(x => x.NewUser.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password is required");
        }
    }
}
