using Application.Models;
using MediatR;

namespace Application.Commands.UserChallengeCommands.Delete
{
    public class DeleteUserChallengeCommand : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }
        public DeleteUserChallengeCommand(int id)
        {
            Id = id;
        }
    }
}
