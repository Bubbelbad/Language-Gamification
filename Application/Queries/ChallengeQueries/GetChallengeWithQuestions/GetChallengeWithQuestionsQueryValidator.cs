using FluentValidation;

namespace Application.Queries.ChallengeQueries.GetChallengeWithQuestions
{
    internal class GetChallengeWithQuestionsQueryValidator : AbstractValidator<GetChallengeWithQuestionsQuery>
    {
        public GetChallengeWithQuestionsQueryValidator()
        {
            RuleFor(x => x.ChallengeId)
                .GreaterThan(0)
                .WithMessage("Id must be of int greater than 0");
        }
    }
}
