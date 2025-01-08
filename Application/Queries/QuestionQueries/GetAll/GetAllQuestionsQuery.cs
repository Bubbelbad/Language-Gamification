using Application.Dtos.QuestionDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.QuestionQueries.GetAll
{
    public class GetAllQuestionsQuery : IRequest<OperationResult<List<GetQuestionDto>>>
    {
    }
}
