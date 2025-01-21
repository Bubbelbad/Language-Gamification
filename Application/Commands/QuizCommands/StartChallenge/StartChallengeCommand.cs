using Application.Models;
using MediatR;

namespace Application.Commands.QuizCommands.StartChallenge
{
    public class StartChallengeCommand(int challengeId, string userId) : IRequest<OperationResult<int>>
    {
        public int ChallengeId { get; set; } = challengeId;
        public string UserId { get; set; } = userId;
    }
}
