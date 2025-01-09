using Application.Dtos.QuestionDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.QuestionCommands.Update
{
    public sealed class UpdateQuestionCommand(UpdateQuestionDto dto) : IRequest<OperationResult<GetQuestionDto>>
    {
        public UpdateQuestionDto Dto { get; set; } = dto; 
    }
}
