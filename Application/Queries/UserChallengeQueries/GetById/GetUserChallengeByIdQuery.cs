using Application.Dtos.UserChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.UserChallengeQueries.GetById
{
    public class GetUserChallengeByIdQuery : IRequest<OperationResult<GetUserChallengeDto>>
    {
        public int Id { get; set; }
        public GetUserChallengeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
