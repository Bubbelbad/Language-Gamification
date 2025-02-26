using Application.Dtos.AnswerDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.AnswerQueries.GetAnswersByQuestionId
{
    public class GetAnswersByQuestionIdQuery(int id) : IRequest<OperationResult<List<GetSimpleAnswerDto>>>
    {
        public int QuestionId { get; set; } = id; 
    }
}