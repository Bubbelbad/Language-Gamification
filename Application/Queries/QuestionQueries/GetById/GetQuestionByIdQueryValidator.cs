using FluentValidation;

namespace Application.Queries.QuestionQueries.GetById
{
    public class GetQuestionByIdQueryValidator : AbstractValidator<GetQuestionByIdQuery>
    {
        public GetQuestionByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");
        }
    }
}
