using Application.Dtos.ChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.ChallengeCommands.Update
{
    public sealed class UpdateChallengeCommand(UpdateChallengeDto dto) : IRequest<OperationResult<GetChallengeDto>>
    {
        public UpdateChallengeDto Dto { get; set; } = dto; 
    }
}
