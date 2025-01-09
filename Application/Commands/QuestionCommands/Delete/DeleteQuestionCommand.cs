using Application.Models;
using MediatR;

namespace Application.Commands.QuestionCommands.Delete
{
    public sealed class DeleteQuestionCommand(int id) : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; } = id; 
    }
}
