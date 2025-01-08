using FluentValidation;

namespace Application.Queries.AnswerQueries.GetAnswerById
{
    public class GetAnswerByIdQueryValidator : AbstractValidator<GetAnswerByIdQuery>
    {
        public GetAnswerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .NotNull().WithMessage("Id is required");
        }
    }
}
