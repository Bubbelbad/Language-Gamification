using Application.Dtos.ChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.ChallengeQueries.GetChallengeById
{
    public class GetChallengeByIdQuery : IRequest<OperationResult<GetChallengeDto>>
    {
        public int Id { get; set; }

        public GetChallengeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
