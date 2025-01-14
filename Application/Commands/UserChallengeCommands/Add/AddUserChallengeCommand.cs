using Application.Dtos.UserChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.UserChallengeCommands.Add
{
    public class AddUserChallengeCommand(AddUserChallengeDto dto) : IRequest<OperationResult<GetUserChallengeDto>>
    {
        public AddUserChallengeDto Dto { get; set; } = dto;
    }
}
