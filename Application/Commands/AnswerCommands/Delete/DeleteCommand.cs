using Application.Models;
using MediatR;

namespace Application.Commands.AnswerCommands.Delete
{
    public class DeleteCommand : IRequest<OperationResult<bool>>
    {
        public int AnswerId { get; set; }

        public DeleteCommand(int answerId)
        {
            AnswerId = answerId;
        }
    }
}
