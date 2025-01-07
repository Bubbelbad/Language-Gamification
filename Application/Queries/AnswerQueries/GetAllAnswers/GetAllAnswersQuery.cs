using Application.Dtos.AnswerDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.AnswerQueries.GetAllAnswers
{
    public class GetAllAnswersQuery() : IRequest<OperationResult<List<GetAnswerDto>>>
    {
    }
}
