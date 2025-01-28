using Application.Dtos.ChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.ChallengeQueries.GetChallengeWithQuestions
{
    public class GetChallengeWithQuestionsQuery(int challengeId) : IRequest<OperationResult<GetChallengeWithQuestionsDto>>
    {
        public int ChallengeId { get; set; } = challengeId;
    }
}
