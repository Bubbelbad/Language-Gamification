
using Application.Dtos.QuestionDtos;
using Application.Models;
using MediatR;

namespace Application.Queries.QuizQueries
{
    public class GetNextQuestionQuery(int userChallengeId) : IRequest <OperationResult<GetQuestionWithAnswersDto>>
    {
        public int UserChallengeId { get; set; } = userChallengeId;
        //public int CurrentQuestionIndex { get; set; }

        //public GetNextQuestionQuery(int userChallengeId, int currentQuestionIndex)
        //{
        //    UserChallengeId = userChallengeId;
        //    CurrentQuestionIndex = currentQuestionIndex;
        //}
    }
}
