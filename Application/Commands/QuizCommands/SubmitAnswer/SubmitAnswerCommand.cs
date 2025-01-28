using Application.Dtos.QuizDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.QuizCommands.SubmitAnswer
{
    public class SubmitAnswerCommand(int userChallengeId, int answerId) : IRequest<OperationResult<SubmitAnswerDto>>
    {
        public int UserChallengeId { get; } = userChallengeId;
        public int AnswerId { get; } = answerId;
    }
}
