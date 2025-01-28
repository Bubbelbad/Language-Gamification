using FluentValidation;

namespace Application.Queries.QuestionQueries.GetQuestionsByChallengeId
{
    internal class GetQuestionsByChallengeIdQueryValidator : AbstractValidator<GetQuestionsByChallengeIdQuery>
    {
        public GetQuestionsByChallengeIdQueryValidator()
        {
            RuleFor(x => x.ChallengeId)
                .GreaterThan(0)
                .WithMessage("Id must be of int greater than 0");
        }
    }
}
