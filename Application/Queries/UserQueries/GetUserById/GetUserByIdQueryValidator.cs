using FluentValidation;

namespace Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .NotNull().WithMessage("Id is required");

        }
    }
}
