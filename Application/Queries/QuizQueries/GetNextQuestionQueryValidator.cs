using FluentValidation;

namespace Application.Queries.QuizQueries
{
    public class GetNextQuestionQueryValidator : AbstractValidator<GetNextQuestionQuery>
    {
        public GetNextQuestionQueryValidator() 
        {
            RuleFor(x => x.UserChallengeId)
                .GreaterThan(0)
                .WithMessage("UserChallengeId must be greater than 0.");

            //RuleFor(x => x.CurrentQuestionIndex).GreaterThanOrEqualTo(0)
            //    .WithMessage("CurrentQuestionIndex cannot be negative.");
        }
    }
}
