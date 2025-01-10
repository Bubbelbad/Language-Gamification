using Application.Dtos.ChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.ChallengeQueries.GetAllChallenges
{
    public class GetAllChallengesQuery() : IRequest<OperationResult<List<GetChallengeDto>>>
    {
    }
}
