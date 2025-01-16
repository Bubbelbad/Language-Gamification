
using Application.Dtos.QuestionDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.QuizQueries
{
    public class GetNextQuestionQuery : IRequest <OperationResult<GetQuestionDto>>
    {
        public int UserChallengeId { get; set; }
        public int CurrentQuestionIndex { get; set; }

        public GetNextQuestionQuery(int userChallengeId, int currentQuestionIndex)
        {
            UserChallengeId = userChallengeId;
            CurrentQuestionIndex = currentQuestionIndex;
        }
    }
}
