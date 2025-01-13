using Application.Dtos.ChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.ChallengeCommands.Update
{
    internal sealed class UpdateChallengeCommandHandler(IGenericRepository<Challenge, int> repository, IMapper mapper) : IRequestHandler<UpdateChallengeCommand, OperationResult<GetChallengeDto>>
    {
        private readonly IGenericRepository<Challenge, int> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<GetChallengeDto>> Handle(UpdateChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var challengeToUpdate = _mapper.Map<Challenge>(request.Dto);
                var updateSuccessful = await _repository.UpdateAsync(challengeToUpdate);

                if (updateSuccessful is null)
                {
                    return OperationResult<GetChallengeDto>.Failure("Failed to update challenge");
                }

                var updatedChallengeDto = _mapper.Map<GetChallengeDto>(challengeToUpdate);
                return OperationResult<GetChallengeDto>.Success(updatedChallengeDto);
            }
            catch (Exception e)
            {
                return OperationResult<GetChallengeDto>.Failure(e.Message);
            }
        }
    }
}
