using Application.Dtos.UserChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserChallengeCommands.Update
{
    public class UpdateUserChallengeCommandHandler : IRequestHandler<UpdateUserChallengeCommand, OperationResult<GetUserChallengeDto>>
    {
        private readonly IGenericRepository<UserChallenge, int> _repository;
        private readonly IMapper _mapper;

        public UpdateUserChallengeCommandHandler(IGenericRepository<UserChallenge, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OperationResult<GetUserChallengeDto>> Handle(UpdateUserChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userChallengeToUpdate = _mapper.Map<UserChallenge>(request.Dto);
                var updateSuccessful = await _repository.UpdateAsync(userChallengeToUpdate);

                if (updateSuccessful is null)
                {
                    return OperationResult<GetUserChallengeDto>.Failure("Failed to update user challenge");
                }

                var updatedUserChallengeDto = _mapper.Map<GetUserChallengeDto>(userChallengeToUpdate);
                return OperationResult<GetUserChallengeDto>.Success(updatedUserChallengeDto);
            }
            catch (Exception e)
            {
                return OperationResult<GetUserChallengeDto>.Failure(e.Message);
            }
        }
    }
}
