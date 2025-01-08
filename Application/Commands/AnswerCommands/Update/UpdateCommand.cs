using Application.Dtos.AnswerDtos;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AnswerCommands.Update
{
    public class UpdateCommand(UpdateAnswerDto answerToUpdate) : IRequest<OperationResult<GetAnswerDto>>
    {
        public UpdateAnswerDto NewAnswer { get; set; } = answerToUpdate;
    }
}
