using Application.Models;
using MediatR;

namespace Application.Commands.ChallengeCommands.Delete
{
    public sealed class DeleteChallengeCommand(int id) : IRequest<OperationResult<bool>>
    {
        public int ChallengeId { get; set; } = id;
    }
}
