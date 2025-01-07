
using Application.Dtos.QuestionDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.QuestionCommands.Add
{
    public class AddQuestionCommand(AddQuestionDto dto) : IRequest<OperationResult<GetQuestionDto>>
    {
        public AddQuestionDto Dto { get; set; } = dto; 
    }
}
