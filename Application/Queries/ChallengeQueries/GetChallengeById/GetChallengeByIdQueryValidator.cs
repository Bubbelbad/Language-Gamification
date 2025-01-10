using FluentValidation;

namespace Application.Queries.ChallengeQueries.GetChallengeById
{
    public class GetChallengeByIdQueryValidator : AbstractValidator<GetChallengeByIdQuery>
    {
        public GetChallengeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .NotNull().WithMessage("Id is required");
        }
    }
}
