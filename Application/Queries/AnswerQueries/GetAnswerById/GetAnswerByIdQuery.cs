using Application.Dtos.AnswerDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.AnswerQueries.GetAnswerById
{
    public class GetAnswerByIdQuery(int id) : IRequest<OperationResult<GetAnswerDto>>
    {
        public int Id { get; set; } = id;
    }
}
