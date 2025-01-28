using Application.Dtos.QuestionDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.QuestionQueries.GetQuestionsByChallengeId
{
    public class GetQuestionsByChallengeIdQuery(int challengeId) : IRequest<OperationResult<List<GetQuestionWithAnswersDto>>>
    {
        public int ChallengeId { get; set; } = challengeId;
    }
}
