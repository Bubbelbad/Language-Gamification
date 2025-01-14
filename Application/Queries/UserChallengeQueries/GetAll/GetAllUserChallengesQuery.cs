using Application.Dtos.UserChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.UserChallengeQueries.GetAll
{
    public class GetAllUserChallengesQuery : IRequest<OperationResult<List<GetUserChallengeDto>>>
    {
    }
}
