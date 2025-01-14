using Application.Dtos.UserChallengeDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.UserChallengeCommands.Update
{
    public class UpdateUserChallengeCommand : IRequest<OperationResult<GetUserChallengeDto>>
    {
        public UpdateUserChallengeDto Dto { get; set; }

        public UpdateUserChallengeCommand(UpdateUserChallengeDto dto)
        {
            Dto = dto;
        }
    }
}
