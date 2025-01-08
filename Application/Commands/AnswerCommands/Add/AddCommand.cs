using Application.Dtos.AnswerDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.AnswerCommands.Add
{
    public class AddCommand(AddAnswerDto newAnswer) : IRequest<OperationResult<GetAnswerDto>>
    {
        public AddAnswerDto NewAnswer { get; set; } = newAnswer;
    }

}
