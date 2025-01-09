using Application.Dtos.QuestionDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.QuestionQueries.GetById
{
    public class GetQuestionByIdQuery : IRequest<OperationResult<GetQuestionDto>>
    {
        public int Id { get; set; }

        public GetQuestionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
