using Application.Models;
using MediatR;

namespace Application.Commands.UserCommands.Delete
{
    public class DeleteCommand : IRequest<OperationResult<bool>>
    {
        public Guid UserId { get; set; }

        public DeleteCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
