using Application.Dtos.ChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.ChallengeCommands.Add
{
    public class AddChallengeCommand(AddChallengeDto dto) : IRequest<OperationResult<GetChallengeDto>>
    {
        public AddChallengeDto Dto { get; set; } = dto;

    }
}
