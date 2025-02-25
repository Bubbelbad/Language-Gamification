using FluentValidation;

namespace Application.Queries.AnswerQueries.GetAnswersByQuestionId
{
    internal class GetAnswersByQuestionIdQueryValidator : AbstractValidator<GetAnswersByQuestionIdQuery>
    {
        public GetAnswersByQuestionIdQueryValidator()
        {
            RuleFor(x => x.QuestionId)
                .GreaterThan(0)
                .WithMessage("Id must be of int greater than 0");
        }
    }
}
