using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AnswerCommands.Submit
{
    public  class SubmitCommandValidator :AbstractValidator<SubmitCommand>
    {
        public SubmitCommandValidator()
        {
            RuleFor(x => x.UserChallengeId)
                .NotEmpty().WithMessage("UserChallengeId cannot be empty.")
                .NotEqual(Guid.Empty).WithMessage("UserChallengeId must be a valid GUID.");

            RuleFor(x => x.SelectedAnswerId)
                .GreaterThan(0).WithMessage("SelectedAnswerId must be a positive integer.");
        }
    }
}
